using System;
using System.Collections.Generic;
using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a task entity in the system.
/// </summary>
public class ProjectTask : AuditableEntity<ProjectTaskId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public override ProjectTaskId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the project identifier associated with this task.
    /// </summary>
    public ProjectId ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this task.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the title of the task.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the estimated time to complete the task (in minutes).
    /// </summary>
    public int? Estimate { get; set; }

    /// <summary>
    /// Gets or sets the cost associated with the task.
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the due date of the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the type of the task.
    /// </summary>
    public TaskType Type { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the priority of the task.
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the user associated with this task.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the project associated with this task.
    /// </summary>
    public Project Project { get; set; } = null!;

    /// <summary>
    /// Gets or sets the workspace associated with this task.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of task assignments.
    /// </summary>
    public ICollection<ProjectTaskAssignment> Assignments { get; set; } = new List<ProjectTaskAssignment>();

    /// <summary>
    /// Gets or sets the collection of assets.
    /// </summary>
    public ICollection<WorkspaceAsset> WorkspaceAssets { get; set; } = new List<WorkspaceAsset>();

    /// <summary>
    /// Gets or sets the is closed flag.
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Gets or sets the payment status of the task.
    /// </summary>
    public TransactionStatus? PaymentStatus { get; set; }
}
