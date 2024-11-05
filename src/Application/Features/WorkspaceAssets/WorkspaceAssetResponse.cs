using Platform.Domain.Entities.Common;

/// <summary>
/// Represents the response from uploading a workspace file.
/// </summary>
public record UploadWorkspaceFileResponse
{
    /// <summary>
    /// Gets or sets the URL of the uploaded file.
    /// </summary>
    public required string FileUrl { get; init; }

    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; init; }
}
