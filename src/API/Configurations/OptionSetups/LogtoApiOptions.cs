using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.API.Configurations.OptionSetups;

/// <summary>
/// Represents the options for Logto API.
/// </summary>
public class LogtoApiOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the URL of the Logto API.
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// Gets or sets the client ID for the Logto API.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret for the Logto API.
    /// </summary>
    public required string ClientSecret { get; set; }
}
