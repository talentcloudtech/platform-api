using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.SearchOpenTasks;

/// <summary>
/// Represents a request to search for open tasks.
/// </summary>
/// <param name="WorkspaceIds">The optional workspace ids to filter tasks by. If null, no filtering by workspace is applied.</param>
/// <param name="ProjectIds">The optional project ids to filter tasks by. If null, no filtering by project is applied.</param>
/// <param name="Search">The optional search to filter tasks by. If null, no filtering by search is applied.</param>
/// <param name="Priorities">The optional priorities to filter tasks by. If null, no filtering by priority is applied.</param>
/// <param name="SortBy">The optional sort by to filter tasks by. If null, no filtering by sort is applied.</param>
/// <param name="SortOrder">The optional sort order to filter tasks by. If null, no filtering by sort order is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <param name="IsPublished">Whether to include published tasks. Defaults to false.</param>
public record SearchOpenTasksRequest(
    WorkspaceId[]? WorkspaceIds,
    ProjectId[]? ProjectIds,
    string? Search = null,
    TaskPriority[]? Priorities = null,
    string SortBy = nameof(ProjectTask.CreatedDate),
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10,
    bool IsPublished = false
) : IRequest<Result<PaginatedList<TaskResponse>>>;
