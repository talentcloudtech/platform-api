using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a task assignment entity in the system.
/// </summary>
public class Proposal : AuditableEntity<ProposalId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the task proposal.
    /// </summary>
    public override ProposalId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the task identifier associated with this assignment.
    /// </summary>
    public ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier associated with this assignment.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the date when the task was assigned.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the proposal.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the cost estimation for the proposal.
    /// </summary>
    public decimal CostEstimation { get; set; }

    /// <summary>
    /// Gets or sets the status of the proposal.
    /// </summary>
    public ProposalStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the task associated with this proposal.
    /// </summary>
    public ProjectTask ProjectTask { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this proposal.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
