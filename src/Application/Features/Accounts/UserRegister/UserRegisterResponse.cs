namespace Platform.Application.Features.Accounts.UserRegister;

/// <summary>
/// Represents a response for user registration.
/// </summary>
public record UserRegisterResponse
{
    /// <summary>
    /// Gets or sets the full name of the user.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the username for the account.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; }
}
