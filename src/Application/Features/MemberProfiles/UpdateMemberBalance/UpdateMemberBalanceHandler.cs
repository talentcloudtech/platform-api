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

namespace Platform.Application.Features.Members.UpdateMemberBalance;

/// <summary>
/// Handles updating member balance
/// </summary>
public class UpdateMemberBalanceHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateMemberBalanceRequest, Result>
{
    /// <summary>
    /// Handles the request to update member balance
    /// </summary>
    public async Task<Result> Handle(UpdateMemberBalanceRequest request, CancellationToken cancellationToken)
    {
        // Verify user has admin permission
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("Only administrators can update member balances.");

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
            return Result.NotFound("User not found.");

        if (user.CurrentBalance < request.Amount)
            return Result.Unavailable("Insufficient balance.");

        using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Update the user's balance
            user.CurrentBalance = (user.CurrentBalance ?? 0) - request.Amount;

            // Create a transaction record
            var memberTransaction = new MemberTransaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Type = request.Amount > 0 ? TransactionType.Credit : TransactionType.Debit,
                Status = TransactionStatus.Success,
                Description = $"Payment reduced from wallet ${request.Amount}",
                PaymentMethod = PaymentMethod.Bank,
                CreatedBy = session.UserId,
                TransactionId = CustomNewId.GenerateStandardId(),
            };

            context.MemberTransactions.Add(memberTransaction);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
