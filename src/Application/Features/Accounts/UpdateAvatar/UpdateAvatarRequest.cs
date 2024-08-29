using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.UpdateAvatar;

/// <summary>
/// Represents a request to update the avatar.
/// </summary>
/// <param name="Url">The url of the new avatar.</param>
public record UpdateAvatarRequest(string Url) : IRequest<Result<string>>;
