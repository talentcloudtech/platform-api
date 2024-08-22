using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.GetMemberProfileById;

/// <summary>
/// Represents a request to get a member profile identified by its <see cref="MemberProfileId"/>.
/// </summary>
/// <param name="Id">The unique identifier of the member profile to be deleted.</param>
public record GetMemberProfileByIdRequest(MemberProfileId Id) : IRequest<Result<MemberProfileResponse>>;
