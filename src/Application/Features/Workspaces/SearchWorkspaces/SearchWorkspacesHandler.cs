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

namespace Platform.Application.Features.Workspaces.SearchWorkspaces;

/// <summary>
/// Handles the request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>A paginated list of workspaces.</returns>
public class SearchWorkspacesHandler(IContext context) : IRequestHandler<SearchWorkspacesRequest, Result<PaginatedList<WorkspaceResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspaces based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating workspaces.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of workspaces.</returns>
    public async Task<Result<PaginatedList<WorkspaceResponse>>> Handle(SearchWorkspacesRequest request, CancellationToken cancellationToken)
    {
        var workspaces = context
            .Workspaces.WhereIf(!string.IsNullOrEmpty(request.Name), x => EF.Functions.Like(x.Name.ToLower().Trim(), $"%{request.Name}%"))
            .AsNoTracking();

        return await workspaces.ProjectToType<WorkspaceResponse>().OrderBy(x => x.Name).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
