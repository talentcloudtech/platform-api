using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a contract entity in the system.
/// </summary>
public class Contract : AuditableEntity<ContractId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the contract.
    /// </summary>
    public override ContractId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the user identifier associated with this contract.
    /// </summary>
    public required UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this contract.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

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
    /// Gets or sets the project associated with this project member.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this project member.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
