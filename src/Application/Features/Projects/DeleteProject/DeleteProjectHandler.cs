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

namespace Platform.Application.Features.Projects.DeleteProject;

/// <summary>
/// Handles the request to delete a project.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the delete operation.</returns>
public class DeleteProjectHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteProjectRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a project.
    /// </summary>
    /// <param name="request">The delete project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.Workspace.WorkspaceMembers)
            .Include(x => x.ProjectMembers)
            .FirstOrDefaultAsync(x => x.WorkspaceId == request.WorkspaceId && x.Id == request.Id, cancellationToken);
        if (project is null)
            return Result.NotFound("Project not found.");

        if (
            !project.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            ) && !project.ProjectMembers.Any(x => x.UserId == session.UserId && (x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectAdmin))
        )
            return Result.Forbidden("You are not authorized to delete this project.");

        context.ProjectMembers.RemoveRange(project.ProjectMembers);
        context.Projects.Remove(project);

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
