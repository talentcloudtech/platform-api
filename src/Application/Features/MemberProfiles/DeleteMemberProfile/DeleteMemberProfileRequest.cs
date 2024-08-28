using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.DeleteMemberProfile;

/// <summary>
/// Represents a request to delete a member profile identified by its <see cref="UserId"/>.
/// </summary>
/// <param name="Id">The unique identifier of the member profile to be deleted.</param>
public record DeleteMemberProfileRequest(UserId Id) : IRequest<Result>;
