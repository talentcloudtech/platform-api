namespace Platform.Application.Features.MemberProfiles.SearchMemberProfiles;

/// <summary>
/// Represents a response for searching member profiles.
/// </summary>
public record SearchMemberProfileResponse
{
    /// <summary>
    /// Gets or sets the full name of the member.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the user name of the member.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email of the member.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the profile picture URL of the member.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }
}
