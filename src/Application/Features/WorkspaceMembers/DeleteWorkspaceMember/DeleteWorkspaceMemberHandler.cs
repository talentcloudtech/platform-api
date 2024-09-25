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
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You are not a owner of this workspace.");

        var member = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Workspace member not found.");

        if (request.UserId == session.UserId)
            return Result.Forbidden("You cannot remove yourself from the workspace.");

        context.WorkspaceMembers.Remove(member);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
