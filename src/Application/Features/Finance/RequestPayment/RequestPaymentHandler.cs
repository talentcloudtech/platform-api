using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Finance.RequestPayment;

/// <summary>
/// Handler for requesting payments for tasks
/// </summary>
public class RequestPaymentHandler(IContext context, ICurrentSession session) : IRequestHandler<RequestPaymentRequest, Result>
{
    /// <summary>
    /// Handles the request to request payments for tasks
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(RequestPaymentRequest request, CancellationToken cancellationToken)
    {
        // Verify workspace membership and permissions
        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == session.UserId,
            cancellationToken
        );

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");

        if (workspaceMember.Role != WorkspaceRole.WorkspaceOwner && workspaceMember.Role != WorkspaceRole.WorkspaceManager)
            return Result.Forbidden("You don't have permission to release payments.");

        // Get tasks with their assignments and workspace
        var tasks = await context
            .ProjectTasks.Include(t => t.Assignments)
            .Include(t => t.Workspace)
            .Where(t => t.WorkspaceId == request.WorkspaceId && request.TaskIds.Contains(t.Id) && !t.IsClosed && t.Cost > 0)
            .ToListAsync(cancellationToken);

        if (!tasks.Any())
            return Result.NotFound("No valid tasks found for payment request.");

        var totalCost = tasks.Sum(t => t.Cost ?? 0);
        var workspace = tasks.First().Workspace;

        // Check workspace balance
        if (workspace.CurrentBalance < totalCost)
            return Result.Error("Insufficient workspace balance for payment release.");

        var currentTime = DateTime.UtcNow;

        foreach (var task in tasks)
        {
            if (task.Assignments.Count > 1)
                return Result.Error($"Task '{task.Title}' has multiple assignees. Payment release requires exactly one assigned user.");

            var assignee = task.Assignments.FirstOrDefault()?.UserId;
            if (assignee == null)
                return Result.Error($"Task '{task.Title}' has no assignee. Payment release requires an assigned user.");

            var member = await context.Users.FirstOrDefaultAsync(m => m.Id == assignee.Value, cancellationToken);

            if (member == null)
                return Result.Error($"Task '{task.Title}' has no assignee. Payment release requires an assigned user.");

            // Create workspace transaction (Debit)
            var workspaceTransaction = new WorkspaceTransaction
            {
                TransactionId = CustomNewId.GenerateStandardId(),
                WorkspaceId = workspace.Id,
                ProjectId = task.ProjectId,
                Amount = task.Cost ?? 0,
                Type = TransactionType.Debit,
                Description = $"Payment requested for task: {task.Title}",
                PaymentMethod = PaymentMethod.Transfer,
                CreatedBy = session.UserId,
                CreatedDate = currentTime,
                UpdatedBy = session.UserId,
                UpdatedDate = currentTime,
                Status = TransactionStatus.Success,
            };

            // Update task status
            task.UpdatedBy = session.UserId;
            task.UpdatedDate = currentTime;
            task.PaymentStatus = TransactionStatus.Pending;

            // Add transactions
            context.WorkspaceTransactions.Add(workspaceTransaction);
            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
