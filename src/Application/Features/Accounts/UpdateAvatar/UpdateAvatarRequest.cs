using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Accounts.UpdateAvatar;

/// <summary>
/// Represents a request to update the avatar.
/// </summary>
/// <param name="FilePath">The file path of the new avatar.</param>
public record UpdateAvatarRequest(string FilePath) : IRequest<Result<string>>;
