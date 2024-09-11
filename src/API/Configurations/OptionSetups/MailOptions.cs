using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.Api.Configurations.OptionSetups;

/// <summary>
/// Represents the configuration options for email settings.
/// </summary>
public class EmailConfigurationOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the email address from which emails will be sent.
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the SMTP server host.
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Gets or sets the SMTP server port.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username for SMTP authentication.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the password for SMTP authentication.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the display name for the sender's email address.
    /// </summary>
    public string? DisplayName { get; set; }
}
