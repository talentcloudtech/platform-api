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
        var user = await context.Users.FindAsync(session.UserId, cancellationToken);
        if (user == null)
            return Result.NotFound("User not found.");
        var workspaceMemberInvitations = context
            .WorkspaceMemberInvitations.Include(x => x.Workspace)
            .Where(x => user.Email != null && x.Email.ToLower().Trim() == user.Email.ToLower().Trim())
            .OrderByDescending(x => x.CreatedDate);

        var workspaceMemberInvitationsResponse = await workspaceMemberInvitations.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceMemberInvitationResponse>
        {
            CurrentPage = workspaceMemberInvitationsResponse.CurrentPage,

            TotalPages = workspaceMemberInvitationsResponse.TotalPages,
            TotalItems = workspaceMemberInvitationsResponse.TotalItems,
            Result = workspaceMemberInvitationsResponse
                .Result.Select(x =>
                {
                    var response = x.Adapt<WorkspaceMemberInvitationResponse>();
                    response.Workspace = x.Workspace.Adapt<WorkspaceResponse>();
                    return response;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
