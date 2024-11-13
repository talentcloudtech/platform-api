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
public class ReleasePaymentHandler : IRequestHandler<ReleasePaymentRequest, Result>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;

    /// <summary>
    /// Constructor for ReleasePaymentHandler
    /// </summary>
    /// <param name="context"></param>
    /// <param name="session"></param>
    public ReleasePaymentHandler(IContext context, ICurrentSession session)
    {
        _context = context;
        _session = session;
    }

    /// <summary>
    /// Handles the request to release payments for tasks
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(ReleasePaymentRequest request, CancellationToken cancellationToken)
    {
        // Verify workspace membership and permissions
        var workspaceMember = await _context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == _session.UserId,
            cancellationToken
        );

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");

        if (workspaceMember.Role != WorkspaceRole.WorkspaceOwner && workspaceMember.Role != WorkspaceRole.WorkspaceManager)
            return Result.Forbidden("You don't have permission to release payments.");

        // Get tasks with their assignments and workspace
        var tasks = await _context
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

        foreach (var task in tasks)
        {
            var assignee = task.Assignments.FirstOrDefault()?.UserId;
            if (assignee == null)
                continue;

            var member = await _context.Users.FirstOrDefaultAsync(m => m.Id == assignee.Value, cancellationToken);

            if (member == null)
                continue;

            // Create member transaction
            var memberTransaction = new MemberTransaction
            {
                TransactionId = CustomNewId.GenerateStandardId(),
                UserId = assignee.Value,
                Amount = task.Cost ?? 0,
                Type = TransactionType.Credit,
                Description = $"Payment received for task: {task.Title}",
                PaymentMethod = PaymentMethod.Transfer,
                CreatedBy = _session.UserId,
                CreatedDate = currentTime,
                UpdatedBy = _session.UserId,
                UpdatedDate = currentTime,
                Status = TransactionStatus.Success,
            };

            var workspaceTransaction = new WorkspaceTransaction
            {
                TransactionId = CustomNewId.GenerateStandardId(),
                WorkspaceId = workspace.Id,
                Amount = task.Cost ?? 0,
                Type = TransactionType.Debit,
                Description = $"Payment released for task: {task.Title}",
                PaymentMethod = PaymentMethod.Transfer,
                CreatedBy = _session.UserId,
                CreatedDate = currentTime,
                UpdatedBy = _session.UserId,
                UpdatedDate = currentTime,
                Status = TransactionStatus.Success,
            };

            // Update task status
            task.IsClosed = true;
            task.UpdatedBy = _session.UserId;
            task.UpdatedDate = currentTime;

            // Update balances
            workspace.CurrentBalance -= task.Cost ?? 0;

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                // Add transactions

                _context.WorkspaceTransactions.Add(workspaceTransaction);
                _context.MemberTransactions.Add(memberTransaction);
                _context.Workspaces.Update(workspace);

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
