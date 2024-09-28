using System;
using System.Linq;
using Ardalis.Result;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities;

/// <summary>
/// Validates the status transition of a gig applicant invitation.
/// </summary>
public class GigApplicantInvitationStatusValidator
{
    /// <summary>
    /// Validates the status transition of a gig applicant invitation.
    /// </summary>
    /// <param name="currentStatus">The current status of the gig applicant invitation.</param>
    /// <param name="newStatus">The new status to transition to.</param>
    /// <param name="isAdmin">Whether the user is an admin.</param>
    /// <param name="isWorkspaceOwnerOrManager">Whether the user is a workspace owner or manager.</param>
    /// <param name="isInvitedUser">Whether the user is the invited user.</param>
    /// <returns>The result of the operation.</returns>
    public static Result ValidateStatusTransition(
        GigApplicantInvitationStatus currentStatus,
        GigApplicantInvitationStatus newStatus,
        bool isAdmin,
        bool isWorkspaceOwnerOrManager,
        bool isInvitedUser
    )
    {
        var validTransitions = GetValidTransitions(currentStatus);
        if (!validTransitions.Contains(newStatus))
        {
            return Result.Error(
                $"Invalid status transition from {currentStatus} to {newStatus}. " + $"Valid transitions are: {string.Join(", ", validTransitions)}"
            );
        }

        if (isAdmin)
        {
            if (
                (
                    currentStatus == GigApplicantInvitationStatus.Pending
                    && (newStatus == GigApplicantInvitationStatus.Accepted || newStatus == GigApplicantInvitationStatus.Declined)
                ) || (currentStatus == GigApplicantInvitationStatus.Accepted && newStatus == GigApplicantInvitationStatus.Shortlisted)
            )
            {
                return Result.Success();
            }
        }
        else if (isWorkspaceOwnerOrManager)
        {
            if (
                (
                    currentStatus == GigApplicantInvitationStatus.Shortlisted
                    && (newStatus == GigApplicantInvitationStatus.InterviewScheduled || newStatus == GigApplicantInvitationStatus.NotSelected)
                )
                || (currentStatus == GigApplicantInvitationStatus.InterviewScheduled && newStatus == GigApplicantInvitationStatus.InInterview)
                || (
                    currentStatus == GigApplicantInvitationStatus.InInterview
                    && (newStatus == GigApplicantInvitationStatus.OfferExtended || newStatus == GigApplicantInvitationStatus.Rejected)
                )
                || (currentStatus == GigApplicantInvitationStatus.OfferAccepted && newStatus == GigApplicantInvitationStatus.Hired)
                || (currentStatus == GigApplicantInvitationStatus.Accepted && newStatus == GigApplicantInvitationStatus.Withdrawn)
            )
            {
                return Result.Success();
            }
        }
        else if (isInvitedUser)
        {
            if (
                currentStatus == GigApplicantInvitationStatus.OfferExtended
                && (newStatus == GigApplicantInvitationStatus.OfferAccepted || newStatus == GigApplicantInvitationStatus.OfferDeclined)
            )
            {
                return Result.Success();
            }
        }

        return Result.Error("You do not have permission to update this status.");
    }

    private static GigApplicantInvitationStatus[] GetValidTransitions(GigApplicantInvitationStatus currentStatus)
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
