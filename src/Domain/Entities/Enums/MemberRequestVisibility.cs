namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the visibility of a member request.
/// </summary>
public enum MemberRequestVisibility
{
    /// <summary>
    /// The member request is public.
    /// </summary>
    Public = 1,

    /// <summary>
    /// The member request is private.
    /// </summary>
    Invited = 2,
}
