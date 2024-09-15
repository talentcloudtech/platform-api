using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.API.Configurations.OptionSetups;

/// <summary>
/// Configuration settings for JWT authentication.
/// These settings are bound from the "JwtSettings" section in appsettings.json.
/// </summary>
public class JwtSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the audience for the JWT.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// Gets or sets the issuer for the JWT.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the lifetime of the access token in minutes.
    /// </summary>
    public required int AccessTokenExpirationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the lifetime of the refresh token in days.
    /// </summary>
    public required int RefreshTokenExpirationDays { get; set; }
}
