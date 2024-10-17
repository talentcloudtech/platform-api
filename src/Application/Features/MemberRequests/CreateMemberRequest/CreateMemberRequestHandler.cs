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

namespace Platform.Application.Features.MemberRequests.CreateMemberRequest;

/// <summary>
/// Handles the creation of a member request.
/// </summary>
public class CreateMemberRequestHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateMemberRequestRequest, Result<MemberRequestResponse>>
{
    /// <summary>
    /// Handles the creation of a member request.
    /// </summary>
    /// <param name="request">The create member request request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the member request creation.</returns>
    public async Task<Result<MemberRequestResponse>> Handle(CreateMemberRequestRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (
            !workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
        )
            return Result.Forbidden("You are not a owner or manager of this workspace. You can't create a member request.");

        var currentTimestamp = DateTime.UtcNow;

        var skills = await context.Skills.Where(x => request.RequiredSkills.Distinct().Contains(x.Id)).ToListAsync(cancellationToken);

        var newMemberRequest = request.Adapt<MemberRequest>();

        newMemberRequest.RequiredSkills = [];
        skills.ForEach(x => newMemberRequest.RequiredSkills.Add(new MemberRequestSkill { SkillId = x.Id, Name = x.Name }));

        newMemberRequest.Status = MemberRequestStatus.Draft;
        newMemberRequest.CreatedBy = session.UserId;
        newMemberRequest.CreatedDate = currentTimestamp;

        context.MemberRequests.Add(newMemberRequest);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newMemberRequest.Adapt<MemberRequestResponse>());
    }
}
