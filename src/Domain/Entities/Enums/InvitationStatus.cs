namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of an invitation.
/// </summary>
public enum InvitationStatus
{
    /// <summary>
    /// The invitation has been sent but not yet responded to.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// The invited user has accepted the invitation.
    /// </summary>
    Accepted = 2,

    /// <summary>
    /// The invited user has declined the invitation.
    /// </summary>
    Declined = 3,

    /// <summary>
    /// The invitation has been withdrawn by the sender.
    /// </summary>
    Withdrawn = 4,

    /// <summary>
    /// The invited user has been shortlisted for further consideration.
    /// </summary>
    Shortlisted = 5,

    /// <summary>
    /// The invited user was not shortlisted and is no longer being considered.
    /// </summary>
    NotShortlisted = 6,

    /// <summary>
    /// An interview has been scheduled with the invited user.
    /// </summary>
    InterviewScheduled = 7,

    /// <summary>
    /// The interview process is currently ongoing with the invited user.
    /// </summary>
    InInterview = 8,

    /// <summary>
    /// An offer has been extended to the invited user.
    /// </summary>
    OfferExtended = 9,

    /// <summary>
    /// The invited user has accepted the offer.
    /// </summary>
    OfferAccepted = 10,

    /// <summary>
    /// The invited user has declined the offer.
    /// </summary>
    OfferDeclined = 11,

    /// <summary>
    /// The invited user has been hired and the process is complete.
    /// </summary>
    Hired = 12,

    /// <summary>
    /// The invited user has been rejected at any stage of the process.
    /// </summary>
    Rejected = 13
}