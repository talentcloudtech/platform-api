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
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMemberInvitations.SearchWorkspaceMemberInvitations;

/// <summary>
/// Handler for searching workspace members.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class SearchWorkspaceMemberInvitationsHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchWorkspaceMemberInvitationsRequest, Result<PaginatedList<WorkspaceMemberInvitationResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspace members.
    /// </summary>
    /// <param name="request">The request to search for workspace members.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The paginated list of workspace members.</returns>
    public async Task<Result<PaginatedList<WorkspaceMemberInvitationResponse>>> Handle(
        SearchWorkspaceMemberInvitationsRequest request,
        CancellationToken cancellationToken
    )
    {
        // Check if the user is a member of the workspace and has role workspace_owner or workspace_manager
        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            x => x.WorkspaceId == request.WorkspaceId && x.UserId == session.UserId,
            cancellationToken
        );
        if (workspaceMember == null || workspaceMember.Role != WorkspaceRole.WorkspaceOwner || workspaceMember.Role != WorkspaceRole.WorkspaceManager)
            return Result.Forbidden("You are not authorized to access this workspace invitations.");

        var query = context.WorkspaceMemberInvitations.Where(x => x.WorkspaceId == request.WorkspaceId);

        if (!string.IsNullOrEmpty(request.Email))
            query = query.Where(x => x.Email.ToLower().Trim() == request.Email.ToLower().Trim());

        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(x => EF.Property<object>(x, request.SortBy));
        else
            query = query.OrderByDescending(x => EF.Property<object>(x, request.SortBy));

        query = query.Include(x => x.Workspace).Include(x => x.User.MemberProfile);

        var workspaceMemberInvitations = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceMemberInvitationResponse>
        {
            CurrentPage = workspaceMemberInvitations.CurrentPage,

            TotalPages = workspaceMemberInvitations.TotalPages,
            TotalItems = workspaceMemberInvitations.TotalItems,
            Result = workspaceMemberInvitations
                .Result.Select(x =>
                {
                    var response = x.Adapt<WorkspaceMemberInvitationResponse>();
                    response.Workspace = x.Workspace.Adapt<WorkspaceResponse>();
                    response.InvitedBy = new CommonUserResponse(x.User, x.User.MemberProfile);
                    return response;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
