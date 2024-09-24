namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of a member request.
/// </summary>
public enum MemberRequestStatus
{
    /// <summary>
    /// The member request is in draft state.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// The member request is pending review.
    /// </summary>
    PendingReview = 2,

    /// <summary>
    /// The member request has been rejected.
    /// </summary>
    Rejected = 3,

    /// <summary>
    /// The member request has been published.
    /// </summary>
    Published = 4,

    /// <summary>
    /// Invitations for the member request have been sent.
    /// </summary>
    InvitationsSent = 5,

    /// <summary>
    /// The member request is in the shortlisting phase.
    /// </summary>
    Shortlisting = 6,

    /// <summary>
    /// Awaiting customer selection for the member request.
    /// </summary>
    AwaitingCustomerSelection = 7,

    /// <summary>
    /// An interview has been scheduled for the member request.
    /// </summary>
    InterviewScheduled = 8,

    /// <summary>
    /// The interview process is ongoing for the member request.
    /// </summary>
    InInterview = 9,

    /// <summary>
    /// An offer has been extended for the member request.
    /// </summary>
    OfferExtended = 10,

    /// <summary>
    /// The offer for the member request has been declined.
    /// </summary>
    OfferDeclined = 11,

    /// <summary>
    /// A candidate has been hired for the member request.
    /// </summary>
    Hired = 12,

    /// <summary>
    /// No candidate was selected for the member request.
    /// </summary>
    NotSelected = 13,

    /// <summary>
    /// The member request has been closed.
    /// </summary>
    Closed = 14,

    /// <summary>
    /// The member request has been cancelled.
    /// </summary>
    Cancelled = 15,
}
