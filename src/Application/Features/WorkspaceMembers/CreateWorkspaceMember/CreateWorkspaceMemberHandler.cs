using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;

/// <summary>
/// Handles the request to create a new workspace member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <param name="emailSender">The email sender.</param>
/// <returns>The result of the operation.</returns>
public class CreateWorkspaceMemberHandler(IContext context, ICurrentSession session, IEmailSender emailSender)
    : IRequestHandler<CreateWorkspaceMemberRequest, Result>
{
    /// <summary>
    /// Handles the request to create a new workspace member.
    /// </summary>
    /// <param name="request">The request to create a new workspace member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(CreateWorkspaceMemberRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Conflict("You are not the owner of this workspace.");

        var workspaceMemberInvitations = new List<WorkspaceMemberInvitation>();

        var currentTimeStamp = DateTime.UtcNow;
        request.WorkspaceMemberInvitations.ForEach(invitation =>
        {
            workspaceMemberInvitations.Add(
                new WorkspaceMemberInvitation
                {
                    Email = invitation.Email,
                    WorkspaceId = request.WorkspaceId,
                    Role = invitation.Role,
                    CreatedBy = session.UserId,
                    CreatedDate = currentTimeStamp,
                }
            );
        });

        await context.WorkspaceMemberInvitations.AddRangeAsync(workspaceMemberInvitations, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await emailSender.SendWorkspaceInvitationAsync(request.WorkspaceId, workspace.Name, workspaceMemberInvitations.Select(x => x.Email).ToList());

        return Result.Success();
    }
}
