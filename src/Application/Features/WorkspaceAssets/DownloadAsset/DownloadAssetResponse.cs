namespace Platform.Application.Features.WorkspaceAssets.DownloadAsset;

/// <summary>
/// Response containing the downloaded asset data
/// </summary>
public class DownloadAssetResponse
{
    /// <summary>
    /// Gets or sets the file content as a byte array
    /// </summary>
    public byte[] Content { get; set; } = null!;

    /// <summary>
    /// Gets or sets the content type of the file
    /// </summary>
    public string ContentType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the original file name
    /// </summary>
    public string FileName { get; set; } = null!;
}
