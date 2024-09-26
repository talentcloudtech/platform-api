using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.WorkspaceMembers.GetWorkspaceMemberById;

/// <summary>
/// Handles the request to get a workspace member by their ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class GetWorkspaceMemberByIdHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetWorkspaceMemberByIdRequest, Result<WorkspaceMemberResponse>>
{
    /// <summary>
    /// Handles the request to get a workspace member by their ID.
    /// </summary>
    /// <param name="request">The request to get a workspace member by their ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The workspace member.</returns>

    public async Task<Result<WorkspaceMemberResponse>> Handle(GetWorkspaceMemberByIdRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context
            .Workspaces.Include(x => x.WorkspaceMembers)
            .ThenInclude(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this workspace.");

        var member = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Workspace member not found.");

        var response = member.Adapt<WorkspaceMemberResponse>();
        response.User = new CommonUserResponse(member.User, member.User.MemberProfile);
        return Result.Success(response);
    }
}
