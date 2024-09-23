using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Represents a request to search for projects with optional filtering and pagination.
/// </summary>
/// <param name="Title">The optional title to filter projects by. If null, no filtering by title is applied.</param>
/// <param name="Statuses">The optional status to filter projects by. If null, no filtering by status is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
public record SearchProjectsRequest(string? Title = null, ProjectStatus[]? Statuses = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ProjectResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID to filter projects by.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the list of statuses to filter projects by.
    /// </summary>
    public ProjectStatus[] Statuses { get; init; } = Statuses ?? new[] { ProjectStatus.Draft, ProjectStatus.InProgress };
}
