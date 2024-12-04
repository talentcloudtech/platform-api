using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces.SearchWorkspacesList;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.SearchWorkspacesList;

/// <summary>
/// Handles the request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>A paginated list of workspaces.</returns>
public class SearchWorkspacesListHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchWorkspacesListRequest, Result<PaginatedList<WorkspaceResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspaces based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating workspaces.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of workspaces.</returns>
    public async Task<Result<PaginatedList<WorkspaceResponse>>> Handle(SearchWorkspacesListRequest request, CancellationToken cancellationToken)
    {
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You are not authorized to access this resource.");

        IQueryable<Workspace> query = context.Workspaces.Include(i => i.User.MemberProfile).Include(i => i.WorkspaceMembers);

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Name.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var workspaces = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceResponse>
        {
            CurrentPage = workspaces.CurrentPage,
            TotalPages = workspaces.TotalPages,
            TotalItems = workspaces.TotalItems,
            Result = workspaces
                .Result.Select(x =>
                {
                    var workspaceResponse = x.Adapt<WorkspaceResponse>();
                    workspaceResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    workspaceResponse.MemberRole = x.WorkspaceMembers.FirstOrDefault(m => m.UserId == session.UserId)?.Role ?? WorkspaceRole.WorkspaceMember;
                    return workspaceResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
