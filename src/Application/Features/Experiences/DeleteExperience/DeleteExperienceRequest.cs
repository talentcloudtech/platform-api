using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Experiences.DeleteExperience;

/// <summary>
/// Represents a request to delete an experience.
/// </summary>
public record DeleteExperienceRequest(UserId MemberProfileId, string ExperienceId) : IRequest<Result>;
