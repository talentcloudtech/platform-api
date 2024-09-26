using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.DeleteWorkspace;

/// <summary>
/// Handles the deletion of a workspace.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class DeleteWorkspaceHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteWorkspaceRequest, Result>
{
    /// <summary>
    /// Handles the deletion of a workspace.
    /// </summary>
    /// <param name="request">The request containing workspace details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(DeleteWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context
            .Workspaces.Include(x => x.WorkspaceMembers)
            .Include(x => x.Projects)
            .ThenInclude(x => x.ProjectMembers)
            .FirstOrDefaultAsync(predicate: w => w.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
        {
            return Result.NotFound("Workspace not found.");
        }
        if (!workspace.WorkspaceMembers.Any(m => m.UserId == session.UserId && m.Role == WorkspaceRole.WorkspaceOwner))
        {
            return Result.Forbidden("You are not allowed to delete this workspace.");
        }

        context.WorkspaceMembers.RemoveRange(workspace.WorkspaceMembers);
        context.ProjectMembers.RemoveRange(workspace.Projects.SelectMany(p => p.ProjectMembers));
        context.Projects.RemoveRange(workspace.Projects);
        context.Workspaces.Remove(workspace);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
