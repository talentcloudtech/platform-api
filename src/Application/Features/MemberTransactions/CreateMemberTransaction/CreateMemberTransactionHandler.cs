using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities.Enums;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.MemberTransactions;
using Platform.Application.Features.MemberTransactions.CreateMemberTransaction;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceTransactions.CreateMemberTransaction;

/// <summary>
/// Handler for creating a new workspace transaction.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class CreateMemberTransactionHandler(IContext context, ICurrentSession session)
    : IRequestHandler<CreateMemberTransactionRequest, Result<MemberTransactionResponse>>
{
    /// <summary>
    /// Handles the request to create a new workspace transaction.
    /// </summary>
    /// <param name="request">The request to create a new workspace transaction.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<MemberTransactionResponse>> Handle(CreateMemberTransactionRequest request, CancellationToken cancellationToken)
    {
        var currentTimeStamp = DateTime.UtcNow;

        if (session.Role!.Name != UserRole.User.Name)
            return Result.Forbidden("User does not have permission to create a member transaction.");

        var newTransaction = new MemberTransaction
        {
            UserId = session.UserId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Type = TransactionType.Credit,
            Description = $"Transaction created at {currentTimeStamp:yyyy-MM-dd}",
            TransactionId = Guid.NewGuid().ToString(),
            Status = TransactionStatus.Pending,
            CreatedBy = session.UserId,
            CreatedDate = currentTimeStamp,
        };

        context.MemberTransactions.Add(newTransaction);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newTransaction.Adapt<MemberTransactionResponse>());
    }
}
