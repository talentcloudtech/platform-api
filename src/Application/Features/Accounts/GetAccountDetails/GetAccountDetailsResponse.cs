using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Accounts.GetAccountDetails;

/// <summary>
/// Represents the response containing account details.
/// </summary>
public class GetAccountDetailsResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the account.
    /// </summary>
    public UserId Id { get; set; }

    /// <summary>
    /// Gets or sets the username associated with the account.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the first name associated with the account.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name associated with the account.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address associated with the account.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the user's profile picture URL.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the user's role.
    /// </summary>

    public string? UserRole { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email is confirmed.
    /// </summary>
    public bool EmailConfirmed { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether the member profile is complete.
    /// </summary>
    public bool IsProfileComplete { get; set; }

    /// <summary>
    /// Gets or sets the completeness score of the member profile.
    /// </summary>
    public int ProfileCompletenessScore { get; set; }
}
