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

namespace Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;

/// <summary>
/// Handles the request to create a new workspace member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <returns>The result of the operation.</returns>
public class CreateWorkspaceMemberHandler(IContext context, ICurrentSession session)
    : IRequestHandler<CreateWorkspaceMemberRequest, Result<WorkspaceMemberResponse>>
{
    /// <summary>
    /// Handles the request to create a new workspace member.
    /// </summary>
    /// <param name="request">The request to create a new workspace member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<WorkspaceMemberResponse>> Handle(CreateWorkspaceMemberRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Conflict("You are not the owner of this workspace.");

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "User not found.",
                    ErrorCode = "USER_NOT_FOUND",
                }
            );

        if (request.UserId == session.UserId)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "You cannot add yourself to the workspace.",
                    ErrorCode = "USER_ALREADY_MEMBER",
                }
            );

        if (workspace.Members.Any(x => x.UserId == request.UserId))
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "UserId",
                    ErrorMessage = "User already a member of the workspace.",
                    ErrorCode = "USER_ALREADY_MEMBER",
                }
            );

        var newMember = request.Adapt<WorkspaceMember>();
        newMember.CreatedBy = session.UserId;
        newMember.CreatedDate = DateTime.UtcNow;

        context.WorkspaceMembers.Add(newMember);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newMember.Adapt<WorkspaceMemberResponse>());
    }
}
