using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Handles the request to search for projects with optional filtering and pagination.
/// </summary>
/// <param name="context">The context to access the database.</param>
/// <param name="session">The current session.</param>
public class SearchProjectsHandler(IContext context, ICurrentSession session) : IRequestHandler<SearchProjectsRequest, Result<PaginatedList<ProjectResponse>>>
{
    /// <summary>
    /// Handles the request to search for projects based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating projects.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<ProjectResponse>>> Handle(SearchProjectsRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Project> query = context
            .Projects.Include(i => i.Workspace)
            .Include(i => i.User.MemberProfile)
            .Where(x => x.WorkspaceId == request.WorkspaceId);

        //Apply workspace and project role filter
        query = query.Where(x =>
            x.Workspace.WorkspaceMembers.Any(m =>
                m.UserId == session.UserId && (m.Role == WorkspaceRole.WorkspaceOwner || m.Role == WorkspaceRole.WorkspaceManager)
            ) || x.ProjectMembers.Any(m => m.UserId == session.UserId)
        );

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply start date filter
        if (request.StartDate != null)
            query = query.Where(x => x.CreatedDate >= request.StartDate);

        // Apply end date filter
        if (request.EndDate != null)
            query = query.Where(x => x.CreatedDate <= request.EndDate);

        // Apply status filter
        if (request.Statuses!.Any())
            query = query.Where(x => request.Statuses!.Contains(x.Status));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var projects = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<ProjectResponse>
        {
            CurrentPage = projects.CurrentPage,
            TotalPages = projects.TotalPages,
            TotalItems = projects.TotalItems,
            Result = projects
                .Result.Select(x =>
                {
                    var projectResponse = x.Adapt<ProjectResponse>();
                    projectResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    projectResponse.Workspace = x.Workspace.Adapt<WorkspaceResponse>();
                    return projectResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
