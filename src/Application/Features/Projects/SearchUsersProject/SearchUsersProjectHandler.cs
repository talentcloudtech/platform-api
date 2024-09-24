using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Projects.SearchProjects;

/// <summary>
/// Handles the request to search for projects with optional filtering and pagination.
/// </summary>
/// <param name="context">The context to access the database.</param>
/// <param name="session">The current session.</param>
public class SearchUsersProjectHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchUsersProjectRequest, Result<PaginatedList<ProjectResponse>>>
{
    /// <summary>
    /// Handles the request to search for projects based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating projects.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="ProjectResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<ProjectResponse>>> Handle(SearchUsersProjectRequest request, CancellationToken cancellationToken)
    {
        var projects = context
            .Projects.Where(x => x.Workspace.WorkspaceMembers.Any(m => m.UserId == session.UserId))
            .WhereIf(request.WorkspaceIds.Any(), x => request.WorkspaceIds.Contains(x.WorkspaceId))
            .WhereIf(request.ProjectIds.Any(), x => request.ProjectIds.Contains(x.Id))
            .WhereIf(!string.IsNullOrEmpty(request.Title), x => EF.Functions.Like(x.Title, $"%{request.Title}%"))
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status));

        return await projects.ProjectToType<ProjectResponse>().OrderBy(x => x.Title).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
