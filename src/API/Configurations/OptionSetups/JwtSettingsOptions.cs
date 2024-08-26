using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.API.Configurations.OptionSetups;

/// <summary>
/// Configuration settings for JWT authentication.
/// These settings are bound from the "JwtSettings" section in appsettings.json.
/// </summary>
public class JwtSettingsOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the authority URL for JWT validation.
    /// </summary>
    public required string Authority { get; set; }

    /// <summary>
    /// Gets or sets the audience for the JWT.
    /// </summary>
    public required string Audience { get; set; }
}
