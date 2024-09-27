using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.WorkspaceMemberInvitations.UpdateWorkspaceMemberInvitation;

/// <summary>
/// Handler for updating a workspace member invitation.
/// </summary>
/// <param name="context">The context.</param>
/// <returns>The result.</returns>
public class UpdateWorkspaceMemberInvitationHandler(IContext context) : IRequestHandler<UpdateWorkspaceMemberInvitationRequest, Result>
{
    /// <summary>
    /// Handles the request to update a workspace member invitation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result.</returns>
    public async Task<Result> Handle(UpdateWorkspaceMemberInvitationRequest request, CancellationToken cancellationToken)
    {
        var workspaceMemberInvitation = await context.WorkspaceMemberInvitations.FirstOrDefaultAsync(
            x => x.Id == request.WorkspaceMemberInvitationId,
            cancellationToken
        );
        if (workspaceMemberInvitation == null)
            return Result.NotFound("Workspace member invitation not found.");

        var user = await context.Users.FirstOrDefaultAsync(
            x => x.Email != null && x.Email.ToLower().Trim() == workspaceMemberInvitation.Email.ToLower().Trim(),
            cancellationToken
        );
        if (user == null)
            return Result.NotFound("User not found.");

        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            x => x.WorkspaceId == workspaceMemberInvitation.WorkspaceId && x.UserId == user.Id,
            cancellationToken
        );
        if (workspaceMember != null)
            return Result.Conflict("User is already a member of the workspace.");

        if (request.Accept)
        {
            var newWorkspaceMember = new WorkspaceMember
            {
                WorkspaceId = workspaceMemberInvitation.WorkspaceId,
                UserId = user.Id,
                Role = workspaceMemberInvitation.Role,
                CreatedBy = user.Id,
                CreatedDate = DateTime.UtcNow,
            };
            context.WorkspaceMembers.Add(newWorkspaceMember);
        }
        context.WorkspaceMemberInvitations.Remove(workspaceMemberInvitation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
