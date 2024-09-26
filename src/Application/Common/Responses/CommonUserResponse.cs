using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Common.Responses;

/// <summary>
/// A common user response for API responses.
/// </summary>
public class CommonUserResponse
{
    /// <summary>
    /// Get or set the user ID.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Get or set the user name.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Get or set the first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Get or set the last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Get or set the profile picture URL.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommonUserResponse"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="memberProfile">The member profile.</param>
    public CommonUserResponse(ApplicationUser user, MemberProfile? memberProfile)
    {
        var nameParts = memberProfile?.FullName?.Split(' ');
        UserId = user.Id;
        UserName = user.UserName;
        FirstName = nameParts?.Length > 0 ? nameParts[0] : user.UserName;
        ;
        LastName = nameParts?.Length > 1 ? nameParts[1] : null;
        ;
        ProfilePictureUrl = memberProfile?.ProfilePictureUrl;
    }
}
