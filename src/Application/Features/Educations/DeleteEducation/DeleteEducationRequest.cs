using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Educations.DeleteEducation;

/// <summary>
/// Represents the request to delete an education.
/// <param name="MemberProfileId">The unique identifier of the member profile.</param>
/// <param name="EducationId">The unique identifier of the education.</param>
/// </summary>
public record DeleteEducationRequest(UserId MemberProfileId, EducationId EducationId) : IRequest<Result>;
