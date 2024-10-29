using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.UpdateProject;

/// <summary>
/// Handles the update project request.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the update operation.</returns>
public class UpdateProjectHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateProjectRequest, Result<ProjectResponse>>
{
    /// <summary>
    /// Handles the update project request.
    /// </summary>
    /// <param name="request">The update project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<ProjectResponse>> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var originalProject = await context
            .Projects.Include(x => x.Workspace.WorkspaceMembers)
            .Include(x => x.ProjectMembers)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalProject == null)
            return Result.NotFound("Project not found.");

        if (
            !originalProject.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
            && !originalProject.ProjectMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectAdmin || x.Role == ProjectRole.ProjectManager)
            )
        )
            return Result.Forbidden("You are not authorized to update this project.");

        originalProject.Title = request.Title;
        originalProject.Description = request.Description;
        originalProject.StartDate = request.StartDate;
        originalProject.EndDate = request.EndDate;
        originalProject.Budget = request.Budget;
        originalProject.Status = request.Status;
        originalProject.Type = request.Type;
        originalProject.Technologies = request.Technologies;
        originalProject.UpdatedBy = session.UserId;
        originalProject.UpdatedDate = DateTime.UtcNow;

        context.Projects.Update(originalProject);
        await context.SaveChangesAsync(cancellationToken);

        return originalProject.Adapt<ProjectResponse>();
    }
}
