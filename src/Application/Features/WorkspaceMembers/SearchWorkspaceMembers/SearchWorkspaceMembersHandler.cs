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

namespace Platform.Application.Features.WorkspaceMembers.SearchWorkspaceMembers;

/// <summary>
/// Handler for searching workspace members.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class SearchWorkspaceMembersHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchWorkspaceMembersRequest, Result<PaginatedList<WorkspaceMemberResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspace members.
    /// </summary>
    /// <param name="request">The request to search for workspace members.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The paginated list of workspace members.</returns>
    public async Task<Result<PaginatedList<WorkspaceMemberResponse>>> Handle(SearchWorkspaceMembersRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.Members.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this workspace.");

        var workspaceMembers = context
            .WorkspaceMembers.Include(x => x.User.MemberProfile)
            .WhereIf(
                !string.IsNullOrEmpty(request.UserName),
                x =>
                    !string.IsNullOrEmpty(request.UserName)
                    && !string.IsNullOrEmpty(x.User.UserName)
                    && EF.Functions.Like(x.User.UserName.ToLower().Trim(), $"%{request.UserName.ToLower().Trim()}%")
            )
            .OrderBy(x => x.User.UserName);

        var workspaceMembersResponse = await workspaceMembers.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceMemberResponse>
        {
            CurrentPage = workspaceMembersResponse.CurrentPage,

            TotalPages = workspaceMembersResponse.TotalPages,
            TotalItems = workspaceMembersResponse.TotalItems,
            Result = workspaceMembersResponse
                .Result.Select(x =>
                {
                    var nameParts = x.User.MemberProfile?.FullName?.Split(' ');
                    var memberResponse = x.Adapt<WorkspaceMemberResponse>();
                    memberResponse.User = new CommonUserResponse
                    {
                        UserId = x.UserId,
                        UserName = x.User.UserName,
                        Email = x.User.Email,
                        FirstName = nameParts?.Length > 0 ? nameParts[0] : null,
                        LastName = nameParts?.Length > 1 ? nameParts[1] : null,
                        ProfilePictureUrl = x.User.MemberProfile?.ProfilePictureUrl,
                    };
                    return memberResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
