using static Platform.API.Configurations.ServiceCollectionExtensions;

namespace Platform.API.Configurations.OptionSetups;

/// <summary>
/// Configuration settings for Azure Blob Storage.
/// These settings are bound from the "AzureBlobStorage" section in appsettings.json.
/// </summary>
public class AzureBlobStorageOptions : IConfigurableOptions
{
    /// <summary>
    /// Gets or sets the connection string for Azure Blob Storage.
    /// </summary>
    public required string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the name of the temporary container.
    /// </summary>
    public required string TempContainer { get; set; }

    /// <summary>
    /// Gets or sets the name of the profile picture container.
    /// </summary>
    public required string ProfileContainer { get; set; }

    /// <summary>
    /// Gets or sets the name of the member resume container.
    /// </summary>
    public required string ResumeContainer { get; set; }
}
