using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.UploadWorkspaceAvatar;

/// <summary>
/// Represents a request to upload a workspace avatar.
/// </summary>
public record UploadWorkspaceAvatarRequest : IRequest<Result<UploadWorkspaceAvatarResponse>>
{
    /// <summary>
    /// Gets or sets the current user ID.
    /// </summary>
    public UserId CurrentUserId { get; set; }

    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the file to be uploaded.
    /// This property is required and must be provided as part of the form data.
    /// </summary>
    public required IFormFile File { get; set; }
}
