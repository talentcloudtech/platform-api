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
/// <param name="userManager">The user manager.</param>
public class UpdateGigOpportunityCandidateHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager)
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
        var currentUser = await context.Users.FirstOrDefaultAsync(x => x.Id == session.UserId, cancellationToken);
        if (currentUser == null)
            return Result.Unauthorized("User not found.");

        // check if the current user is an admin
        var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");

        var memberRequest = await context.MemberRequests.FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);

        if (memberRequest == null)
            return Result.NotFound("Member request not found.");

        var gigInvitation = await context.GigApplicantInvitations.FirstOrDefaultAsync(
            x => x.MemberRequestId == request.MemberRequestId && x.InvitedUserId == request.UserId,
            cancellationToken
        );
        if (gigInvitation == null)
            return Result.NotFound("Gig invitation not found.");

        var statusTransitionResult = ValidateStatusTransition(gigInvitation.Status, request.Status);
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

    private Result ValidateStatusTransition(GigApplicantInvitationStatus currentStatus, GigApplicantInvitationStatus newStatus)
    {
        var validTransitions = GetValidTransitions(currentStatus);
        if (!validTransitions.Contains(newStatus))
        {
            return Result.Error(
                $"Invalid status transition from {currentStatus} to {newStatus}. " + $"Valid transitions are: {string.Join(", ", validTransitions)}"
            );
        }
        return Result.Success();
    }

    private GigApplicantInvitationStatus[] GetValidTransitions(GigApplicantInvitationStatus currentStatus)
    {
        return currentStatus switch
        {
            GigApplicantInvitationStatus.Pending => new[] { GigApplicantInvitationStatus.Accepted, GigApplicantInvitationStatus.Declined },
            GigApplicantInvitationStatus.Accepted => new[] { GigApplicantInvitationStatus.Shortlisted, GigApplicantInvitationStatus.Withdrawn },
            GigApplicantInvitationStatus.Shortlisted => new[] { GigApplicantInvitationStatus.InterviewScheduled, GigApplicantInvitationStatus.NotSelected },
            GigApplicantInvitationStatus.InterviewScheduled => new[] { GigApplicantInvitationStatus.InInterview },
            GigApplicantInvitationStatus.InInterview => new[] { GigApplicantInvitationStatus.OfferExtended, GigApplicantInvitationStatus.Rejected },
            GigApplicantInvitationStatus.OfferExtended => new[] { GigApplicantInvitationStatus.OfferAccepted, GigApplicantInvitationStatus.OfferDeclined },
            GigApplicantInvitationStatus.OfferAccepted => new[] { GigApplicantInvitationStatus.Hired },
            GigApplicantInvitationStatus.Declined => Array.Empty<GigApplicantInvitationStatus>(),
            GigApplicantInvitationStatus.Withdrawn => Array.Empty<GigApplicantInvitationStatus>(),
            GigApplicantInvitationStatus.NotSelected => Array.Empty<GigApplicantInvitationStatus>(),
            GigApplicantInvitationStatus.Rejected => Array.Empty<GigApplicantInvitationStatus>(),
            GigApplicantInvitationStatus.OfferDeclined => Array.Empty<GigApplicantInvitationStatus>(),
            GigApplicantInvitationStatus.Hired => Array.Empty<GigApplicantInvitationStatus>(),
            _ => Array.Empty<GigApplicantInvitationStatus>(),
        };
    }
}
