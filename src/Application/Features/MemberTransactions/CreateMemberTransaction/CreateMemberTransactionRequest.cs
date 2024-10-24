using Ardalis.Result;
using Domain.Entities.Enums;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.MemberTransactions.CreateMemberTransaction;

/// <summary>
/// Represents a request to create a workspace transaction.
/// </summary>
public record CreateMemberTransactionRequest : IRequest<Result<MemberTransactionResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the member.
    /// </summary>
    [JsonIgnore]
    public UserId MemberId { get; init; }

    /// <summary>
    /// Gets or sets the amount of the transaction.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Gets or sets the payment method of the transaction.
    /// </summary>
    public required PaymentMethod PaymentMethod { get; init; }
}
