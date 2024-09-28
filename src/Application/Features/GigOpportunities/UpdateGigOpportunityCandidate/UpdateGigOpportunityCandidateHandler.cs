using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.GigInvitations;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.UpdateGigOpportunityCandidate;

/// <summary>
/// Handles the request to update the status of a gig opportunity candidate.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class UpdateGigOpportunityCandidateHandler(IContext context, ICurrentSession session)
    : IRequestHandler<UpdateGigOpportunityCandidateRequest, Result<GigInvitationResponse>>
{
    /// <summary>
    /// Handles the request to update the status of a gig opportunity candidate.
    /// </summary>
    /// <param name="request">The request to update the status of a gig opportunity candidate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<GigInvitationResponse>> Handle(UpdateGigOpportunityCandidateRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);

        if (memberRequest == null)
            return Result.NotFound("Member request not found.");

        var gigInvitation = await context.GigApplicantInvitations.FirstOrDefaultAsync(
            x => x.MemberRequestId == request.MemberRequestId && x.InvitedUserId == request.UserId,
            cancellationToken
        );
        if (gigInvitation == null)
            return Result.NotFound("Gig invitation not found.");

        var isAdmin = session.Role!.Name == UserRole.Admin.Name;
        var isWorkspaceOwnerOrManager = memberRequest.Workspace.WorkspaceMembers.Any(x =>
            x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
        );
        var isInvitedUser = session.UserId == gigInvitation.InvitedUserId;

        var statusTransitionResult = GigApplicantInvitationStatusValidator.ValidateStatusTransition(
            gigInvitation.Status,
            request.Status,
            isAdmin,
            isWorkspaceOwnerOrManager,
            isInvitedUser
        );
        if (!statusTransitionResult.IsSuccess)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "status",
                    ErrorMessage = statusTransitionResult.Errors.FirstOrDefault(),
                    ErrorCode = "InvalidStatusTransition",
                }
            );

        gigInvitation.Status = request.Status;
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(gigInvitation.Adapt<GigInvitationResponse>());
    }
}
