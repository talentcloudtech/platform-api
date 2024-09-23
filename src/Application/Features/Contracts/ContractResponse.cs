using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts;

/// <summary>
/// Represents the response model for a contract in the application.
/// </summary>
public record ContractResponse
{
    /// <summary>
    /// Gets the unique identifier for the contract.
    /// </summary>
    public ContractId Id { get; set; }

    /// <summary>
    /// Gets or sets the user identifier associated with this contract.
    /// </summary>
    public required UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the billing type for this contract.
    /// </summary>
    public required BillingType BillingType { get; set; }

    /// <summary>
    /// Gets or sets the available hours or units per day for this contract.
    /// </summary>
    public int AvailablePerDay { get; set; }

    /// <summary>
    /// Gets or sets the billing rate for this contract.
    /// </summary>
    public int BillingRate { get; set; }

    /// <summary>
    /// Gets or sets the start date of the contract.
    /// </summary>
    public required DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the contract. Null if the contract is ongoing.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the workspace member associated with this contract.
    /// </summary>
    public required WorkspaceMemberStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date when the contract was created.
    /// </summary>
    public required DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the contract.
    /// </summary>
    public required UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the contract was last updated. Null if never updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated the contract. Null if never updated.
    /// </summary>
    public UserId? UpdatedBy { get; set; }
}
