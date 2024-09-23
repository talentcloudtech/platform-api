using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a task assignment entity in the system.
/// </summary>
public class ProjectTaskAssignment : AuditableEntity<ProjectTaskAssignmentId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the task assignment.
    /// </summary>
    public override ProjectTaskAssignmentId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the task identifier associated with this assignment.
    /// </summary>
    public ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier associated with this assignment.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the time spent on the task (in minutes).
    /// </summary>
    public int TimeSpent { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who assigned the task.
    /// </summary>
    public UserId AssignedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the task was assigned.
    /// </summary>
    public DateTime AssignedDate { get; set; }

    /// <summary>
    /// Gets or sets the task associated with this assignment.
    /// </summary>
    public ProjectTask ProjectTask { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this assignment.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
