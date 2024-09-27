using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Workspaces.GetUserWorkspaceRole;

/// <summary>
/// Handler for getting the role of a user in a workspace.
/// </summary>
/// <param name="context">The database context. </param>
/// <param name="session">The current session. </param>
public class GetUserWorkspaceRoleHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetUserWorkspaceRoleRequest, Result<WorkspaceMemberRoleResponse>>
{
    /// <summary>
    /// Handles the request to get the role of a user in a workspace.
    /// </summary>
    /// <param name="request">The request to get the role of a user in a workspace.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The role of the user in the workspace. </returns>
    public async Task<Result<WorkspaceMemberRoleResponse>> Handle(GetUserWorkspaceRoleRequest request, CancellationToken cancellationToken)
    {
        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == session.UserId,
            cancellationToken
        );
        if (workspaceMember == null)
            return Result.NotFound("Workspace member role not found.");

        return Result.Success(new WorkspaceMemberRoleResponse { Role = workspaceMember.Role });
    }
}
