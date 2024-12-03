using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Members.UpdateMemberBalance;

/// <summary>
/// Request to update member balance
/// </summary>
public record UpdateMemberBalanceRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the user ID whose balance needs to be updated
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the amount to update (positive for credit, negative for debit)
    /// </summary>
    public required decimal Amount { get; init; }
}
