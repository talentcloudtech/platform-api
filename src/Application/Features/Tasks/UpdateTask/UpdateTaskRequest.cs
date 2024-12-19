using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.UpdateTask;

/// <summary>
/// Represents a request to update an existing task.
/// </summary>
public record UpdateTaskRequest : IRequest<Result<TaskResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier associated with this task.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project identifier associated with this task.
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the project task identifier.
    /// </summary>
    [JsonIgnore]
    public ProjectTaskId Id { get; init; }

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
    /// Gets or sets the is closed flag.
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Gets or sets the type of the task.
    /// </summary>
    public TaskType Type { get; set; }

    /// <summary>
    /// Gets or sets the open task status.
    /// </summary>
    public OpenTaskStatus OpenTaskStatus { get; set; }
}
