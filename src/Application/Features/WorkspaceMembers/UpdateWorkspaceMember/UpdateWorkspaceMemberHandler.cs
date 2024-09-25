using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMembers.UpdateWorkspaceMember;

/// <summary>
/// Handler for updating a workspace member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The updated workspace member.</returns>
public class UpdateWorkspaceMemberHandler(IContext context, ICurrentSession session)
    : IRequestHandler<UpdateWorkspaceMemberRequest, Result<WorkspaceMemberResponse>>
{
    /// <summary>
    /// Handles the request to update a workspace member.
    /// </summary>
    /// <param name="request">The request to update a workspace member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated workspace member.</returns>
    public async Task<Result<WorkspaceMemberResponse>> Handle(UpdateWorkspaceMemberRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        var updateRequestWorkspaceMember = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (updateRequestWorkspaceMember == null)
            return Result.NotFound("Workspace member not found.");

        var currentWorkspaceMember = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == session.UserId);
        if (currentWorkspaceMember == null || currentWorkspaceMember.Role != WorkspaceRole.WorkspaceOwner)
            return Result.Forbidden("You are not authorized to update this workspace member.");

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return Result.NotFound("User not found.");

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "Role",
                    ErrorMessage = "You cannot change your own role in the workspace.",
                    ErrorCode = "SELF_ROLE_CHANGE",
                }
            );

        if (currentWorkspaceMember.Role == WorkspaceRole.WorkspaceManager && request.Role == WorkspaceRole.WorkspaceOwner)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "Role",
                    ErrorMessage = "You are not authorized to change the role to workspace owner.",
                    ErrorCode = "UNAUTHORIZED_ROLE_CHANGE",
                }
            );

        if (currentWorkspaceMember.Role == WorkspaceRole.WorkspaceManager && updateRequestWorkspaceMember.Role == WorkspaceRole.WorkspaceOwner)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "Role",
                    ErrorMessage = "You are not authorized to change the role of workspace owners.",
                    ErrorCode = "UNAUTHORIZED_ROLE_CHANGE",
                }
            );

        updateRequestWorkspaceMember.Role = request.Role;
        updateRequestWorkspaceMember.UpdatedBy = session.UserId;
        updateRequestWorkspaceMember.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(updateRequestWorkspaceMember.Adapt<WorkspaceMemberResponse>());
    }
}
