using System;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Tasks;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Represents a response for a task assignee.
/// </summary>
public class TaskAssigneeResponse
{
    /// <summary>
    /// Gets or sets the ID of the task assignee.
    /// </summary>
    public ProjectTaskAssignmentId Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the task.
    /// </summary>
    public ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the time spent on the task.
    /// </summary>
    public int? TimeSpent { get; set; }

    /// <summary>
    /// Gets or sets the created date of the task assignee.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the task.
    /// </summary>
    public TaskResponse? Task { get; set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public CommonUserResponse? User { get; set; }
}
