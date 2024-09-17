using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.UpdateAvatar;

/// <summary>
/// Represents a request to update the avatar.
/// </summary>
public record UpdateAvatarRequest : IRequest<Result<UpdateAvatarResponse>>
{
    /// <summary>
    /// Get or set user id
    /// </summary>
    public required UserId Id { get; set; }

    /// <summary>
    /// Gets or sets the file to be uploaded.
    /// This property is required and must be provided as part of the form data.
    /// </summary>
    public required IFormFile File { get; set; }
}
