using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMembers.DeleteWorkspaceMember;

/// <summary>
/// Handler for deleting a workspace member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class DeleteWorkspaceMemberHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteWorkspaceMemberRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a workspace member.
    /// </summary>
    /// <param name="request">The request to delete a workspace member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(DeleteWorkspaceMemberRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces
        .Include(x => x.WorkspaceMembers)
        .Include(x => x.Projects)
            .ThenInclude(p => p.ProjectMembers)
            .Include(x => x.Projects)
                .ThenInclude(p => p.ProjectTasks)
                .ThenInclude(t => t.Assignments)
        .FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);



        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        var currentWorkspaceMember = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == session.UserId);
        if (
            currentWorkspaceMember == null
            || (currentWorkspaceMember.Role != WorkspaceRole.WorkspaceOwner && currentWorkspaceMember.Role != WorkspaceRole.WorkspaceManager)
        )
            return Result.Forbidden("You are not authorized to remove members from this workspace.");

        var deleteRequestWorkspaceMember = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (deleteRequestWorkspaceMember == null)
            return Result.NotFound("Workspace member not found.");

        if (request.UserId == session.UserId)
            return Result.Forbidden("You cannot remove yourself from the workspace.");

        if (currentWorkspaceMember.Role == WorkspaceRole.WorkspaceManager && deleteRequestWorkspaceMember.Role == WorkspaceRole.WorkspaceOwner)
            return Result.Forbidden("You are not authorized to remove workspace owner.");

        using var memberRemove = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var projectMembersToRemove = workspace.Projects
        .SelectMany(p => p.ProjectMembers)
        .Where(pm => pm.UserId == request.UserId);
            context.ProjectMembers.RemoveRange(projectMembersToRemove);

            // Remove from task assignments
            var taskAssignmentsToRemove = workspace.Projects
                .SelectMany(p => p.ProjectTasks)
                .SelectMany(t => t.Assignments)
                .Where(a => a.UserId == request.UserId);

            context.ProjectTaskAssignments.RemoveRange(taskAssignmentsToRemove);


            context.WorkspaceMembers.Remove(deleteRequestWorkspaceMember);
            await context.SaveChangesAsync(cancellationToken);
            await memberRemove.CommitAsync(cancellationToken);
        }
        catch
        {
            await memberRemove.RollbackAsync(cancellationToken);
            throw;
        }

        return Result.Success();
    }
}
