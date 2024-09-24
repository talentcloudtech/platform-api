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
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.CreateProject;

/// <summary>
/// Handles the creation of a project.
/// </summary>
public class CreateProjectHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateProjectRequest, Result<ProjectResponse>>
{
    /// <summary>
    /// Handles the creation of a project.
    /// </summary>
    /// <param name="request">The create project request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the project creation.</returns>
    public async Task<Result<ProjectResponse>> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You are not a owner of this workspace. You can't create a project.");

        var currentTimestamp = DateTime.UtcNow;

        var newProject = request.Adapt<Project>();
        newProject.Status = ProjectStatus.Draft;
        newProject.CreatedBy = session.UserId;
        newProject.CreatedDate = currentTimestamp;
        newProject.ProjectMembers.Add(
            new ProjectMember
            {
                ProjectId = newProject.Id,
                UserId = session.UserId,
                Role = ProjectRole.ProjectOwner,
                CreatedBy = session.UserId,
                CreatedDate = currentTimestamp,
            }
        );

        context.Projects.Add(newProject);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newProject.Adapt<ProjectResponse>());
    }
}
