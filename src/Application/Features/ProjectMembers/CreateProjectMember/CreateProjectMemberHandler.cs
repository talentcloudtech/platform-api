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

namespace Platform.Application.Features.ProjectMembers.CreateProjectMember;

/// <summary>
/// Handles the request to create a new project member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <returns>The result of the operation.</returns>
public class CreateProjectMemberHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateProjectMemberRequest, Result<ProjectMemberResponse>>
{
    /// <summary>
    /// Handles the request to create a new project member.
    /// </summary>
    /// <param name="request">The request to create a new project member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<ProjectMemberResponse>> Handle(CreateProjectMemberRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.Members)
            .Include(x => x.Workspace.Members)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (
            !project.Workspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner)
            && !project.Members.Any(x =>
                x.UserId == session.UserId && (x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectAdmin || x.Role == ProjectRole.ProjectManager)
            )
        )
            return Result.Forbidden("You don't have permission to add members to this project.");

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorMessage = "User not found.",
                    ErrorCode = "USER_NOT_FOUND",
                }
            );

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorMessage = "You cannot add yourself to the project.",
                    ErrorCode = "SELF_ADD",
                }
            );

        if (project.Members.Any(x => x.UserId == request.UserId))
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorMessage = "User is already a member of the project.",
                    ErrorCode = "USER_ALREADY_MEMBER",
                }
            );

        var newMember = request.Adapt<ProjectMember>();
        newMember.CreatedBy = session.UserId;
        newMember.CreatedDate = DateTime.UtcNow;

        context.ProjectMembers.Add(newMember);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newMember.Adapt<ProjectMemberResponse>());
    }
}
