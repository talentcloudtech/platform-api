namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of a member request.
/// </summary>
public enum MemberRequestStatus
{
    /// <summary>
    /// The request is in draft mode.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// The request is active.
    /// </summary>
    Active = 2,

    /// <summary>
    /// The request is cancelled.
    /// </summary>
    Cancelled = 3,

    /// <summary>
    /// The request is on-boarding.
    /// </summary>
    Onboarding = 4,

    /// <summary>
    /// The request is in interview.
    /// </summary>
    Interview = 5,
}
