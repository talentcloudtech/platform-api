using System.Text.Json.Serialization;

namespace Platform.Domain.Services.Logto;

/// <summary>
/// Represents the request to the Logto authentication endpoint.
/// </summary>
public class AuthenticationRequest
{
    /// <summary>
    /// The access token.
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    /// <summary>
    /// In seconds.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// The type of the token.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = null!;

    /// <summary>
    /// The scope of the token.
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = null!;
}
