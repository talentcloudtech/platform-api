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
}
