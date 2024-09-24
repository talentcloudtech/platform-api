using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Represents a request to search for projects with optional filtering and pagination.
/// </summary>
/// <param name="WorkspaceIds">The optional workspace ids to filter projects by. If null, no filtering by workspace is applied.</param>
/// <param name="ProjectIds">The optional project ids to filter projects by. If null, no filtering by project is applied.</param>
/// <param name="Title">The optional title to filter projects by. If null, no filtering by title is applied.</param>
/// <param name="Statuses">The optional status to filter projects by. If null, no filtering by status is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
public record SearchUsersProjectRequest(
    WorkspaceId[]? WorkspaceIds,
    ProjectId[]? ProjectIds,
    string? Title = null,
    ProjectStatus[]? Statuses = null,
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<ProjectResponse>>>
{
    /// <summary>
    /// Gets or sets the list of workspace ids to filter projects by.
    /// </summary>
    public WorkspaceId[] WorkspaceIds { get; init; } = WorkspaceIds ?? [];

    /// <summary>
    /// Gets or sets the list of project ids to filter projects by.
    /// </summary>
    public ProjectId[] ProjectIds { get; init; } = ProjectIds ?? [];

    /// <summary>
    /// Gets or sets the list of statuses to filter projects by.
    /// </summary>
    public ProjectStatus[] Statuses { get; init; } = Statuses ?? new[] { ProjectStatus.Draft, ProjectStatus.InProgress };
}
