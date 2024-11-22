using System;
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

namespace Platform.Application.Features.Finance.ReleasePayment;

/// <summary>
/// Handler for releasing payments for tasks
/// </summary>
public class ReleasePaymentHandler(IContext context, ICurrentSession session) : IRequestHandler<ReleasePaymentRequest, Result>
{
    /// <summary>
    /// Handles the request to release payments for tasks
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(ReleasePaymentRequest request, CancellationToken cancellationToken)
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
            return Result.NotFound("No valid tasks found for payment release.");

        var totalCost = tasks.Sum(t => t.Cost ?? 0);
        var workspace = tasks.First().Workspace;

        // Check workspace balance
        if (workspace.CurrentBalance < totalCost)
            return Result.Error("Insufficient workspace balance for payment release.");

        var currentTime = DateTime.UtcNow;

        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            foreach (var task in tasks)
            {

                if (task.Assignments.Count > 1)
                    return Result.Error($"Task '{task.Title}' has multiple assignees. Payment release requires exactly one assigned user.");

                var assignee = task.Assignments.FirstOrDefault()?.UserId;
                if (assignee == null)
                    return Result.Error($"Task '{task.Title}' has no assignee. Payment release requires an assigned user.");





                var member = await context.Users.FirstOrDefaultAsync(m => m.Id == assignee.Value, cancellationToken);

                if (member == null)
                    continue;

                // Deduct from workspace balance
                workspace.CurrentBalance -= task.Cost ?? 0;
                context.Workspaces.Update(workspace);

                // Add to member balance
                member.CurrentBalance = member.CurrentBalance ?? 0 + task.Cost ?? 0;
                context.Users.Update(member);

                // Create member transaction (Credit)
                var memberTransaction = new MemberTransaction
                {
                    TransactionId = CustomNewId.GenerateStandardId(),
                    UserId = assignee.Value,
                    Amount = task.Cost ?? 0,
                    Type = TransactionType.Credit,
                    Description = $"Payment received for task: {task.Title}",
                    PaymentMethod = PaymentMethod.Transfer,
                    CreatedBy = session.UserId,
                    CreatedDate = currentTime,
                    UpdatedBy = session.UserId,
                    UpdatedDate = currentTime,
                    Status = TransactionStatus.Success,
                };

                // Create workspace transaction (Debit)
                var workspaceTransaction = new WorkspaceTransaction
                {
                    TransactionId = CustomNewId.GenerateStandardId(),
                    WorkspaceId = workspace.Id,
                    ProjectId = task.ProjectId,
                    Amount = task.Cost ?? 0,
                    Type = TransactionType.Debit,
                    Description = $"Payment released for task: {task.Title}",
                    PaymentMethod = PaymentMethod.Transfer,
                    CreatedBy = session.UserId,
                    CreatedDate = currentTime,
                    UpdatedBy = session.UserId,
                    UpdatedDate = currentTime,
                    Status = TransactionStatus.Success,
                };

                // Update task status
                task.IsClosed = true;
                task.DueDate = currentTime;
                task.UpdatedBy = session.UserId;
                task.UpdatedDate = currentTime;

                // Add transactions
                context.WorkspaceTransactions.Add(workspaceTransaction);
                context.MemberTransactions.Add(memberTransaction);
                await context.SaveChangesAsync(cancellationToken);
            }
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw; // Re-throw to handle at higher level
        }

        return Result.Success();
    }
}
