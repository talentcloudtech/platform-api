namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the type of a member request.
/// </summary>
public enum MemberRequestType
{
    /// <summary>
    /// The member request is an internal request.
    /// </summary>
    Internal = 1,

    /// <summary>
    /// The member request is an external request.
    /// </summary>
    External = 2,
}
