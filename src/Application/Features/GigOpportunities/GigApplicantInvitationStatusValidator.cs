using System;
using System.Linq;
using Ardalis.Result;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities;

/// <summary>
/// Validates the status transition of a gig applicant invitation.
/// </summary>
public static class GigApplicantInvitationStatusValidator
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
            return Result.Error(
                $"Invalid status transition from {currentStatus} to {newStatus}. " + $"Valid transitions are: {string.Join(", ", validTransitions)}"
            );

        if (isAdmin)
            return Result.Success();

        if (isWorkspaceOwnerOrManager && IsValidWorkspaceOwnerOrManagerTransition(currentStatus, newStatus))
            return Result.Success();

        if (isInvitedUser && IsValidInvitedUserTransition(currentStatus, newStatus))
            return Result.Success();

        return Result.Error("You do not have permission to update this status.");
    }

    private static bool IsValidWorkspaceOwnerOrManagerTransition(GigApplicantInvitationStatus currentStatus, GigApplicantInvitationStatus newStatus)
    {
        return (currentStatus, newStatus) switch
        {
            (GigApplicantInvitationStatus.Accepted, GigApplicantInvitationStatus.Shortlisted) => true,
            (GigApplicantInvitationStatus.Shortlisted, GigApplicantInvitationStatus.InterviewScheduled) => true,
            (GigApplicantInvitationStatus.Shortlisted, GigApplicantInvitationStatus.NotSelected) => true,
            (GigApplicantInvitationStatus.InterviewScheduled, GigApplicantInvitationStatus.InInterview) => true,
            (GigApplicantInvitationStatus.InInterview, GigApplicantInvitationStatus.OfferExtended) => true,
            (GigApplicantInvitationStatus.InInterview, GigApplicantInvitationStatus.Rejected) => true,
            (GigApplicantInvitationStatus.OfferAccepted, GigApplicantInvitationStatus.Hired) => true,
            _ => false,
        };
    }

    private static bool IsValidInvitedUserTransition(GigApplicantInvitationStatus currentStatus, GigApplicantInvitationStatus newStatus)
    {
        if (newStatus == GigApplicantInvitationStatus.Withdrawn && IsWithdrawableStatus(currentStatus))
            return true;

        return (currentStatus, newStatus) switch
        {
            (GigApplicantInvitationStatus.Pending, GigApplicantInvitationStatus.Accepted) => true,
            (GigApplicantInvitationStatus.Pending, GigApplicantInvitationStatus.Rejected) => true,
            (GigApplicantInvitationStatus.Pending, GigApplicantInvitationStatus.Declined) => true,
            (GigApplicantInvitationStatus.Accepted, GigApplicantInvitationStatus.Withdrawn) => true,
            (GigApplicantInvitationStatus.OfferExtended, GigApplicantInvitationStatus.OfferAccepted) => true,
            (GigApplicantInvitationStatus.OfferExtended, GigApplicantInvitationStatus.OfferDeclined) => true,
            _ => false,
        };
    }

    private static bool IsWithdrawableStatus(GigApplicantInvitationStatus status)
    {
        return status switch
        {
            GigApplicantInvitationStatus.Shortlisted => true,
            GigApplicantInvitationStatus.InterviewScheduled => true,
            GigApplicantInvitationStatus.InInterview => true,
            GigApplicantInvitationStatus.OfferExtended => true,
            GigApplicantInvitationStatus.OfferAccepted => true,
            GigApplicantInvitationStatus.Hired => true,
            _ => false,
        };
    }

    private static GigApplicantInvitationStatus[] GetValidTransitions(GigApplicantInvitationStatus currentStatus)
    {
        return currentStatus switch
        {
            GigApplicantInvitationStatus.Pending => new[] { GigApplicantInvitationStatus.Accepted, GigApplicantInvitationStatus.Declined },
            GigApplicantInvitationStatus.Accepted => new[] { GigApplicantInvitationStatus.Shortlisted, GigApplicantInvitationStatus.Withdrawn },
            GigApplicantInvitationStatus.Shortlisted => new[] { GigApplicantInvitationStatus.InterviewScheduled, GigApplicantInvitationStatus.NotSelected },
            GigApplicantInvitationStatus.InterviewScheduled => new[] { GigApplicantInvitationStatus.InInterview, GigApplicantInvitationStatus.Hired },
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
