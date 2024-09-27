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
        var projects = context
            .Projects.Where(x =>
                x.WorkspaceId == request.WorkspaceId
                && (
                    x.Workspace.WorkspaceMembers.Any(wm =>
                        wm.UserId == session.UserId && (wm.Role == WorkspaceRole.WorkspaceOwner || wm.Role == WorkspaceRole.WorkspaceManager)
                    ) || x.ProjectMembers.Any(pm => pm.UserId == session.UserId)
                )
            )
            .WhereIf(!string.IsNullOrEmpty(request.Title), x => EF.Functions.Like(x.Title, $"%{request.Title}%"))
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status));

        return await projects.ProjectToType<ProjectResponse>().OrderBy(x => x.Title).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
