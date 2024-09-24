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
using Platform.Domain.Entities.Common;
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

        var currentTimeStamp = DateTime.UtcNow;
        var workspaceMemberInvitations = new List<WorkspaceMemberInvitation>();

        var existingEmails = await context
            .WorkspaceMemberInvitations.Where(x => x.WorkspaceId == request.WorkspaceId && request.Emails.Contains(x.Email))
            .Select(x => x.Email)
            .ToListAsync(cancellationToken);

        var newEmails = request.Emails.Except(existingEmails).ToList();

        newEmails.ForEach(email =>
        {
            workspaceMemberInvitations.Add(
                new WorkspaceMemberInvitation
                {
                    Email = email,
                    WorkspaceId = request.WorkspaceId,
                    Role = request.Role,
                    Code = CustomNewId.GenerateShortId(),
                    CreatedBy = session.UserId,
                    CreatedDate = currentTimeStamp,
                }
            );
        });

        await context.WorkspaceMemberInvitations.AddRangeAsync(workspaceMemberInvitations, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        await emailSender.SendWorkspaceInvitationAsync(workspace.Name, workspaceMemberInvitations);

        return Result.Success();
    }
}
