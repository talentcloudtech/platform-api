using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.WorkspaceMemberInvitations.ResendWorkspaceMemberInvitation;

/// <summary>
/// Handles the request to resend a workspace member invitation.
/// </summary>
/// <param name="context">The database context. </param>
/// <param name="session">The current user session. </param>
/// <param name="emailSender">The email sender. </param>
public class ResendWorkspaceMemberInvitationHandler(IContext context, ICurrentSession session, IEmailSender emailSender)
    : IRequestHandler<ResendWorkspaceMemberInvitationRequest, Result>
{
    /// <summary>
    /// Handles the request to resend a workspace member invitation.
    /// </summary>
    /// <param name="request">The request to resend a workspace member invitation. </param>
    /// <param name="cancellationToken">The cancellation token. </param>
    /// <returns>The result of the operation. </returns>
    public async Task<Result> Handle(ResendWorkspaceMemberInvitationRequest request, CancellationToken cancellationToken)
    {
        var invitation = await context
            .WorkspaceMemberInvitations.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.InvitationId, cancellationToken);
        if (invitation == null)
            return Result.NotFound("Invitation not found.");

        if (
            !invitation.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
        )
            return Result.Forbidden("You don't have permission to resend this invitation.");

        await emailSender.SendWorkspaceInvitationAsync(invitation.Workspace.Name, [invitation]);
        return Result.Success();
    }
}
