using System;
using System.Collections.Generic;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Projects;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities;
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
    /// Gets or sets the workspace identifier associated with this task.
    /// </summary>
    public WorkspaceId WorkspaceId { get; init; }

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

    /// <summary>
    /// Gets or sets the workspace associated with this task.
    /// </summary>
    public WorkspaceResponse? Workspace { get; set; }

    /// <summary>
    /// Gets or sets the project associated with this task.
    /// </summary>
    public ProjectResponse? Project { get; set; }

    /// <summary>
    /// Gets or sets the user who created this task.
    /// </summary>
    public CommonUserResponse? CreatedByUser { get; set; }

    /// <summary>
    /// Gets or sets the list of assignees for this task.
    /// </summary>
    public List<CommonUserResponse> Assignees { get; set; } = new();

    /// <summary>
    /// Gets or sets the is closed flag.
    /// </summary>
    public bool IsClosed { get; set; }

    /// <summary>
    /// Gets or sets the type of the task.
    /// </summary>
    public TaskType Type { get; set; }

    /// <summary>
    /// Gets or sets the is published flag.
    /// </summary>
    public OpenTaskStatus OpenTaskStatus { get; set; }

    /// <summary>
    /// Gets or sets the is published flag.
    /// </summary>
    public bool Applied { get; set; }

    /// <summary>
    /// Gets or sets the is published flag.
    /// </summary>
    public ProposalId? ProposalId { get; set; }
}
