using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities.Enums;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;

/// <summary>
/// Handler for creating a new workspace transaction.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class CreateWorkspaceTransactionHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateWorkspaceTransactionRequest, Result<WorkspaceTransactionResponse>>
{
    /// <summary>
    /// Handles the request to create a new workspace transaction.
    /// </summary>
    /// <param name="request">The request to create a new workspace transaction.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<WorkspaceTransactionResponse>> Handle(CreateWorkspaceTransactionRequest request, CancellationToken cancellationToken)
    {
        var currentTimeStamp = DateTime.UtcNow;

        if (session.Role!.Name != UserRole.Admin.Name && session.Role!.Name != UserRole.Customer.Name)
            return Result.Forbidden("User does not have permission to create a workspace transaction.");

        var newTransaction = new WorkspaceTransaction
        {
            WorkspaceId = request.WorkspaceId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Type = TransactionType.Credit, // Assuming it's always a credit transaction
            Description = "Workspace transaction", // You might want to add this to the request if needed
            TransactionId = Guid.NewGuid().ToString(), // Generate a unique transaction ID
            Status = TransactionStatus.Pending, // Set initial status as pending
            CreatedBy = session.UserId,
            CreatedDate = currentTimeStamp
        };

        context.WorkspaceTransactions.Add(newTransaction);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newTransaction.Adapt<WorkspaceTransactionResponse>());
    }
}
