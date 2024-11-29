using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.Workspaces.UpdateWorkspaceCreditBalance;

/// <summary>
/// Request to update workspace credit balance
/// </summary>
public record UpdateWorkspaceCreditBalanceRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the amount to update (positive for credit, negative for debit)
    /// </summary>
    public required decimal Amount { get; init; }
}