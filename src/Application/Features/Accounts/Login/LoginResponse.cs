using System;
using System.Text.Json.Serialization;

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
    /// Gets or sets the refresh token. Not serialized to the client — delivered via an HttpOnly cookie instead.
    /// </summary>
    [JsonIgnore]
    public required string RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the access token.
    /// </summary>
    public int ExpiresIn { get; set; }
}
