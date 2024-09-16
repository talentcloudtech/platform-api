using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Educations.GetEducationById;

/// <summary>
/// Represents the request to get an education by its identifier.
/// </summary>
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="EducationId">The unique identifier of the education.</param>
public record GetEducationByIdRequest(UserId MemberProfileId, string EducationId) : IRequest<Result<EducationResponse>>;
