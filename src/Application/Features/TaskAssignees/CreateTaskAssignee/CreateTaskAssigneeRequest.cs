using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;

/// <summary>
/// Represents a request to update an existing task.
/// </summary>
public record CreateTaskAssigneeRequest : IRequest<Result<TaskAssigneeResponse>>
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
    public ProjectTaskId TaskId { get; init; }

    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the estimated time to complete the task (in minutes).
    /// </summary>
    public int? TimeSpent { get; set; }
}
