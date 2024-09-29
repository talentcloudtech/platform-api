using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMemberInvitations.DeleteWorkspaceMemberInvitation;

/// <summary>
/// Handles the request to delete a workspace member invitation.
/// </summary>
/// <param name="context">The application database context. </param>
/// <param name="session">The current user session. </param>
public class DeleteWorkspaceMemberInvitationHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteWorkspaceMemberInvitationRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a workspace member invitation.
    /// </summary>
    /// <param name="request">The request to delete a workspace member invitation. </param>
    /// <param name="cancellationToken">The cancellation token. </param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(DeleteWorkspaceMemberInvitationRequest request, CancellationToken cancellationToken)
    {
        var invitation = await context
            .WorkspaceMemberInvitations.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.InvitationId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (invitation == null)
            return Result.NotFound("Workspace member invitation not found.");

        if (
            !invitation.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
        )
            return Result.Forbidden("You don't have permission to delete this invitation.");

        context.WorkspaceMemberInvitations.Remove(invitation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
