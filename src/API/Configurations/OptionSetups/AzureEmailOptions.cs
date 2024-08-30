using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.API.Configurations.OptionSetups;

/// <summary>
/// Represents the configuration options for Azure email service.
/// </summary>
public class AzureEmailOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the connection string for the Azure Communication Services resource.
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the email address used as the sender for outgoing emails.
    /// </summary>
    public string? SenderAddress { get; set; }
}
