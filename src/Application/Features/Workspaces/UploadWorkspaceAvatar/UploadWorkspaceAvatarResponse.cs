namespace Platform.Application.Features.Workspaces.UploadWorkspaceAvatar;

/// <summary>
/// Represents the response for the upload workspace avatar operation.
/// </summary>
public class UploadWorkspaceAvatarResponse
{
    /// <summary>
    /// Gets or sets the URL of the uploaded file.
    /// </summary>
    public string FileUrl { get; set; } = null!;
}
