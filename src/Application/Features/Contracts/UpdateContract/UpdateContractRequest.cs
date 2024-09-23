using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts.UpdateContract;

/// <summary>
/// Represents a request to update a contract's details.
/// </summary>
public record UpdateContractRequest : IRequest<Result<ContractResponse>>
{
    /// <summary>
    /// Gets the unique identifier of the contract to be updated.
    /// </summary>
    [JsonIgnore]
    public ContractId Id { get; init; }

    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the user identifier associated with this contract.
    /// </summary>
    public required UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the billing type for this contract.
    /// </summary>
    public required BillingType BillingType { get; init; }

    /// <summary>
    /// Gets or sets the available hours or units per day for this contract.
    /// </summary>
    public int AvailablePerDay { get; init; }

    /// <summary>
    /// Gets or sets the billing rate for this contract.
    /// </summary>
    public int BillingRate { get; init; }

    /// <summary>
    /// Gets or sets the start date of the contract.
    /// </summary>
    public required DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date of the contract. Null if the contract is ongoing.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Gets or sets the status of the workspace member associated with this contract.
    /// </summary>
    public required WorkspaceMemberStatus Status { get; init; }
}
