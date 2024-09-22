using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.SearchTasks;

/// <summary>
/// Represents a request to search for tasks.
/// </summary>
/// <param name="Title">The optional title to filter tasks by. If null, no filtering by title is applied.</param>
/// <param name="Status">The optional status to filter tasks by. If null, no filtering by status is applied.</param>
/// <param name="Priority">The optional priority to filter tasks by. If null, no filtering by priority is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchTasksRequest(string? Title = null, TaskStatus? Status = null, TaskPriority? Priority = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<TaskResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }
}
