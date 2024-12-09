using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.LeaveWorkspace;

/// <summary>
/// Handles the request to leave a workspace
/// </summary>
public class LeaveWorkspaceHandler(IContext context, ICurrentSession session) : IRequestHandler<LeaveWorkspaceRequest, Result>
{
    /// <inheritdoc/>
    /// <param name="request">The request containing workspace details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(LeaveWorkspaceRequest request, CancellationToken cancellationToken)
    {
        // Get workspace member with role
        var workspaceMember = await context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == session.UserId,
            cancellationToken
        );

        if (workspaceMember == null)
            return Result.NotFound("You are not a member of this workspace.");

        // Check if user is workspace owner
        if (workspaceMember.Role == WorkspaceRole.WorkspaceOwner)
            return Result.Error("Workspace owner cannot leave the workspace. Transfer ownership first.");

        // Remove workspace member
        context.WorkspaceMembers.Remove(workspaceMember);

        // Remove from all projects in the workspace
        var projectMembers = await context
            .ProjectMembers.Where(pm => pm.Project.WorkspaceId == request.WorkspaceId && pm.UserId == session.UserId)
            .ToListAsync(cancellationToken);

        if (projectMembers.Any())
            context.ProjectMembers.RemoveRange(projectMembers);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
