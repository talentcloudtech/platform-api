using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Represents a request to search for projects with optional filtering and pagination.
/// </summary>
/// <param name="WorkspaceIds">The optional workspace ids to filter projects by. If null, no filtering by workspace is applied.</param>
/// <param name="ProjectIds">The optional project ids to filter projects by. If null, no filtering by project is applied.</param>
/// <param name="Search">The optional search to filter projects by. If null, no filtering by search is applied.</param>
/// <param name="SortBy">The optional sort by to filter projects by. If null, no filtering by sort is applied.</param>
/// <param name="SortOrder">The optional sort order to filter projects by. If null, no filtering by sort order is applied.</param>
/// <param name="Statuses">The optional status to filter projects by. If null, no filtering by status is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
public record SearchUsersProjectRequest(
    WorkspaceId[]? WorkspaceIds,
    ProjectId[]? ProjectIds,
    string? Search = null,
    string SortBy = nameof(Project.CreatedDate),
    string SortOrder = "desc",
    ProjectStatus[]? Statuses = null,
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<ProjectResponse>>>;
