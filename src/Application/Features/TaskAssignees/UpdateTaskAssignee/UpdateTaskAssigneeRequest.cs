using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Represents a request to update task assignees.
/// </summary>
public record UpdateTaskAssigneeRequest : IRequest<Result<List<TaskAssigneeResponse>>>
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
    /// Gets or sets the list of user IDs to be assigned to the task
    /// </summary>
    public required ICollection<UserId> UserIds { get; init; }
}
