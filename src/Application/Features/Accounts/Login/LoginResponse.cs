using System;

namespace Platform.Application.Features.Accounts.Login;

/// <summary>
/// Represents the response after a successful login.
/// </summary>
public class LoginResponse
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public required string RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token.
    /// </summary>
    public int ExpiresIn { get; set; }
}
