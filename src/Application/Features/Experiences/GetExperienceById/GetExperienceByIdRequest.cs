using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Experiences.GetExperienceById;

/// <summary>
/// Represents a request to get an experience by its unique identifier.
/// </summary>
public record GetExperienceByIdRequest(UserId MemberProfileId, string ExperienceId) : IRequest<Result<ExperienceResponse>>;
