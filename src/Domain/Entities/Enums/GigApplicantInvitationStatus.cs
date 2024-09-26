namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of a gig applicant invitation.
/// </summary>
public enum GigApplicantInvitationStatus
{
    /// <summary>
    /// The invitation is pending a response from the applicant.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// The applicant has accepted the invitation.
    /// </summary>
    Accepted = 2,

    /// <summary>
    /// The applicant has been shortlisted for further consideration.
    /// </summary>
    Shortlisted = 3,

    /// <summary>
    /// An interview has been scheduled with the applicant.
    /// </summary>
    InterviewScheduled = 4,

    /// <summary>
    /// The applicant is currently in the interview process.
    /// </summary>
    InInterview = 5,

    /// <summary>
    /// An offer has been extended to the applicant.
    /// </summary>
    OfferExtended = 6,

    /// <summary>
    /// The applicant has accepted the offer.
    /// </summary>
    OfferAccepted = 7,

    /// <summary>
    /// The applicant has been hired.
    /// </summary>
    Hired = 8,

    /// <summary>
    /// The applicant has declined the invitation.
    /// </summary>
    Declined = 9,

    /// <summary>
    /// The applicant has withdrawn their application after accepting.
    /// </summary>
    Withdrawn = 10,

    /// <summary>
    /// The applicant was not selected after being shortlisted.
    /// </summary>
    NotSelected = 11,

    /// <summary>
    /// The applicant was rejected after the interview.
    /// </summary>
    Rejected = 12,

    /// <summary>
    /// The applicant has declined the offer.
    /// </summary>
    OfferDeclined = 13
}