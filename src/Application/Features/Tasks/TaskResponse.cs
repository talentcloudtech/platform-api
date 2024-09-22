using System;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks;

/// <summary>
/// Represents a task response in the system.
/// </summary>
public class TaskResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the project task.
    /// </summary>
    public ProjectTaskId Id { get; init; }

    /// <summary>
    /// Gets or sets the project identifier associated with this task.
    /// </summary>
    public ProjectId ProjectId { get; init; }

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
    /// Gets or sets the priority of the task.
    /// </summary>
    public TaskPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets the date when the task was created.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this task.
    /// </summary>
    public UserId CreatedBy { get; init; }

    /// <summary>
    /// Gets or sets the date when the task was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this task.
    /// </summary>
    public UserId? UpdatedBy { get; init; }
}
