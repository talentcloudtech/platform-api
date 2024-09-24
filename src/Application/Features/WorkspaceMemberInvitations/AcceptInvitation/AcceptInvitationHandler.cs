using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.WorkspaceMemberInvitations.AcceptInvitation;

/// <summary>
/// Handles the confirmation of an email address.
/// </summary>
public class AcceptInvitationHandler(IContext context) : IRequestHandler<AcceptInvitationRequest, Result<string>>
{
    /// <summary>
    /// Handles the confirmation of an email address.
    /// </summary>
    /// <param name="request">The request to confirm an email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the confirmation process.</returns>
    public async Task<Result<string>> Handle(AcceptInvitationRequest request, CancellationToken cancellationToken)
    {
        var workspaceMemberInvitation = await context.WorkspaceMemberInvitations.FindAsync(request.WorkspaceId, request.WorkspaceMemberInvitationId);

        if (workspaceMemberInvitation == null)
            return Result.Error("Workspace member invitation not found");

        if (workspaceMemberInvitation.Code != request.Code)
            return Result.Error("Invalid invitation code");

        var user = await context.Users.FirstOrDefaultAsync(
            x => x.Email != null && x.Email.ToLower().Trim() == workspaceMemberInvitation.Email.ToLower().Trim(),
            cancellationToken
        );
        if (user == null)
            return Result.NotFound("User not found.");

        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            x => x.WorkspaceId == request.WorkspaceId && x.UserId == user.Id,
            cancellationToken
        );
        if (workspaceMember != null)
            return Result.Conflict("User is already a member of the workspace.");

        var newWorkspaceMember = new WorkspaceMember
        {
            WorkspaceId = request.WorkspaceId,
            UserId = user.Id,
            Role = workspaceMemberInvitation.Role,
            CreatedBy = user.Id,
            CreatedDate = DateTime.UtcNow,
        };
        context.WorkspaceMembers.Add(newWorkspaceMember);
        context.WorkspaceMemberInvitations.Remove(workspaceMemberInvitation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success("Workspace member invitation accepted successfully.");
    }
}
