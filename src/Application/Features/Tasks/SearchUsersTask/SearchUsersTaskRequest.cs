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
/// <param name="WorkspaceIds">The optional workspace ids to filter tasks by. If null, no filtering by workspace is applied.</param>
/// <param name="ProjectIds">The optional project ids to filter tasks by. If null, no filtering by project is applied.</param>
/// <param name="Title">The optional title to filter tasks by. If null, no filtering by title is applied.</param>
/// <param name="Statuses">The optional statuses to filter tasks by. If null, no filtering by status is applied.</param>
/// <param name="Priorities">The optional priorities to filter tasks by. If null, no filtering by priority is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchUsersTaskRequest(
    WorkspaceId[]? WorkspaceIds,
    ProjectId[]? ProjectIds,
    string? Title = null,
    TaskStatus[]? Statuses = null,
    TaskPriority[]? Priorities = null,
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<TaskResponse>>>
{
    /// <summary>
    /// Gets or sets the list of workspace ids to filter tasks by.
    /// </summary>
    public WorkspaceId[] WorkspaceIds { get; init; } = WorkspaceIds ?? [];

    /// <summary>
    /// Gets or sets the list of project ids to filter tasks by.
    /// </summary>
    public ProjectId[] ProjectIds { get; init; } = ProjectIds ?? [];

    /// <summary>
    /// Gets or sets the list of statuses to filter tasks by.
    /// </summary>
    public TaskStatus[] Statuses { get; init; } = Statuses ?? new[] { TaskStatus.ToDo, TaskStatus.InProgress };

    /// <summary>
    /// Gets or sets the list of priorities to filter tasks by.
    /// </summary>
    public TaskPriority[] Priorities { get; init; } = Priorities ?? new[] { TaskPriority.High, TaskPriority.Critical };
}
