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
        var workspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You are not a owner of this workspace.");

        var member = workspace.Members.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "Workspace member not found.",
                    ErrorCode = "WORKSPACE_MEMBER_NOT_FOUND",
                }
            );

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "You cannot remove yourself from the workspace.",
                    ErrorCode = "SELF_REMOVE",
                }
            );

        context.WorkspaceMembers.Remove(member);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
