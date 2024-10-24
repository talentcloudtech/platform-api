using Ardalis.Result;
using Domain.Entities.Enums;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;

/// <summary>
/// Represents a request to create a workspace transaction.
/// </summary>
public record CreateWorkspaceTransactionRequest : IRequest<Result<WorkspaceTransactionResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the workspace.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the amount of the transaction.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Gets or sets the payment method of the transaction.
    /// </summary>
    public required PaymentMethod PaymentMethod { get; init; }
}
