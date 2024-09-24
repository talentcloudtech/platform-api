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

namespace Platform.Application.Features.MemberRequests.UpdateMemberRequest;

/// <summary>
/// Handles the update member request request.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the update operation.</returns>
public class UpdateMemberRequestHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateMemberRequestRequest, Result<MemberRequestResponse>>
{
    /// <summary>
    /// Handles the update member request request.
    /// </summary>
    /// <param name="request">The update member request request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<MemberRequestResponse>> Handle(UpdateMemberRequestRequest request, CancellationToken cancellationToken)
    {
        var originalMemberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalMemberRequest == null)
            return Result.NotFound("Member request not found.");

        if (!originalMemberRequest.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You don't have permission to update this member request.");

        originalMemberRequest.Title = request.Title;
        originalMemberRequest.Description = request.Description;
        originalMemberRequest.StartDate = request.StartDate;
        originalMemberRequest.EndDate = request.EndDate;
        originalMemberRequest.ExpectedBudget = request.ExpectedBudget;
        originalMemberRequest.MinutesPerDay = request.MinutesPerDay;
        originalMemberRequest.Timezone = request.Timezone;
        originalMemberRequest.RequiredSkills = request.RequiredSkills;
        originalMemberRequest.Status = request.Status;
        originalMemberRequest.UpdatedBy = session.UserId;
        originalMemberRequest.UpdatedDate = DateTime.UtcNow;

        context.MemberRequests.Update(originalMemberRequest);
        await context.SaveChangesAsync(cancellationToken);

        return originalMemberRequest.Adapt<MemberRequestResponse>();
    }
}
