using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers.DeleteProjectMember;

/// <summary>
/// Handler for deleting a project member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
public class DeleteProjectMemberHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteProjectMemberRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a project member.
    /// </summary>
    /// <param name="request">The request containing the project and workspace IDs and the user ID to remove.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(DeleteProjectMemberRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (
            !project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner)
            && !project.ProjectMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectAdmin || x.Role == ProjectRole.ProjectManager)
            )
        )
            return Result.Forbidden("You are not authorized to remove members from this project.");

        var member = project.ProjectMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Project member not found.");

        if (request.UserId == session.UserId)
            return Result.NotFound("You cannot remove yourself from the project.");

        context.ProjectMembers.Remove(member);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
