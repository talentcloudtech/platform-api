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

namespace Platform.Application.Features.ProjectMembers.UpdateProjectMember;

/// <summary>
/// Handles the request to update a project member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <returns>The result of the operation.</returns>
public class UpdateProjectMemberHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateProjectMemberRequest, Result<ProjectMemberResponse>>
{
    /// <summary>
    /// Handles the request to update a project member.
    /// </summary>
    /// <param name="request">The request to update a project member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Result<ProjectMemberResponse>> Handle(UpdateProjectMemberRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        var currentUserWorkspaceRole = project.Workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == session.UserId)?.Role;
        var currentUserProjectRole = project.ProjectMembers.FirstOrDefault(x => x.UserId == session.UserId)?.Role;
        if (
            currentUserWorkspaceRole != WorkspaceRole.WorkspaceOwner
            && currentUserWorkspaceRole != WorkspaceRole.WorkspaceManager
            && currentUserProjectRole != ProjectRole.ProjectOwner
            && currentUserProjectRole != ProjectRole.ProjectAdmin
            && currentUserProjectRole != ProjectRole.ProjectManager
        )
            return Result.Forbidden("You don't have permission to update members in this project.");

        var member = project.ProjectMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Project member not found.");

        if (request.UserId == session.UserId)
            return Result.Forbidden("You cannot change your own role or allocated minutes per day in the project.");

        if (
            currentUserWorkspaceRole == null
            && (
                (currentUserProjectRole == ProjectRole.ProjectAdmin && member.Role == ProjectRole.ProjectOwner)
                || (
                    currentUserProjectRole == ProjectRole.ProjectManager && (member.Role == ProjectRole.ProjectAdmin || member.Role == ProjectRole.ProjectOwner)
                )
            )
        )
            return Result.Forbidden("You don't have permission to update members in this project.");

        member.Role = request.Role;
        member.AllocatedMinMinutePerDay = request.AllocatedMinMinutePerDay;
        member.UpdatedBy = session.UserId;
        member.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(member.Adapt<ProjectMemberResponse>());
    }
}
