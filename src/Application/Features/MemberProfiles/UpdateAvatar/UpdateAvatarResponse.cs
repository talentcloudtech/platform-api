namespace Platform.Application.Features.MemberProfiles.UpdateAvatar;

/// <summary>
///  Represents the response for the update avatar operation.
/// </summary>
public class UpdateAvatarResponse
{
    /// <summary>
    /// Gets or sets the URL of the uploaded file.
    /// </summary>
    public string FileUrl { get; set; } = null!;
}
