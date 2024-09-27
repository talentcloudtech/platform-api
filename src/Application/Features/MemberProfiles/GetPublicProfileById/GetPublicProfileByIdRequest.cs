using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.GetPublicProfileById;

/// <summary>
/// Represents a request to get a public member profile identified by its <see cref="UserId"/>.
/// </summary>
/// <param name="Id">The unique identifier of the member profile to be deleted.</param>
public record GetPublicProfileByIdRequest(UserId Id) : IRequest<Result<PublicProfileResponse>>;
