using System;
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

namespace Platform.Application.Features.Workspaces.UpdateWorkspaceCreditBalance;

/// <summary>
/// Handles updating workspace credit balance
/// </summary>
public class UpdateWorkspaceCreditBalanceHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateWorkspaceCreditBalanceRequest, Result>
{
    /// <summary>
    /// Handles the request to update workspace credit balance
    /// </summary>
    public async Task<Result> Handle(UpdateWorkspaceCreditBalanceRequest request, CancellationToken cancellationToken)
    {
        var currentTimeStamp = DateTime.UtcNow;

        // Verify user has permission
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("Only administrators can update workspace credit balance.");

        var workspace = await context.Workspaces.FirstOrDefaultAsync(w => w.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Update the balance
            workspace.CurrentBalance += request.Amount;

            // Create a transaction record
            var workspaceTransaction = new WorkspaceTransaction
            {
                WorkspaceId = request.WorkspaceId,
                Amount = request.Amount,
                Type = TransactionType.Credit,
                Status = TransactionStatus.Success,
                Description = $"Balance credited by Admin.",
                PaymentMethod = PaymentMethod.Transfer,
                CreatedBy = session.UserId,
                CreatedDate = currentTimeStamp,
                TransactionId = CustomNewId.GenerateStandardId(),
            };

            context.WorkspaceTransactions.Add(workspaceTransaction);
            context.Workspaces.Update(workspace);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return Result.Success();
    }
}
