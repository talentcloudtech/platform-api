using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        if (
            !project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner)
            && !project.ProjectMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectAdmin || x.Role == ProjectRole.ProjectManager)
            )
        )
            return Result.Forbidden("You don't have permission to update members in this project.");

        var member = project.ProjectMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorMessage = "Project member not found.",
                    ErrorCode = "PROJECT_MEMBER_NOT_FOUND",
                }
            );

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.Role),
                    ErrorMessage = "You cannot change your own role or allocated minutes per day in the project.",
                    ErrorCode = "SELF_ROLE_OR_MINUTES_CHANGE",
                }
            );

        member.Role = request.Role;
        member.AllocatedMinMinutePerDay = request.AllocatedMinMinutePerDay;
        member.UpdatedBy = session.UserId;
        member.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(member.Adapt<ProjectMemberResponse>());
    }
}
