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
using Platform.Domain.Enums;

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
        var workspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        var member = workspace.Members.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "Workspace member not found.",
                    ErrorCode = "WORKSPACE_MEMBER_NOT_FOUND",
                }
            );

        if (!workspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Conflict("You are not the owner of this workspace.");

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "User not found.",
                    ErrorCode = "USER_NOT_FOUND",
                }
            );

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "Role",
                    ErrorMessage = "You cannot change your own role in the workspace.",
                    ErrorCode = "SELF_ROLE_CHANGE",
                }
            );

        member.Role = request.Role;

        member.UpdatedBy = session.UserId;
        member.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(member.Adapt<WorkspaceMemberResponse>());
    }
}
