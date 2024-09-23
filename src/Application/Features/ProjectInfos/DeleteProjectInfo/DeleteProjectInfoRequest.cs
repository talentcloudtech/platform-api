using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectInfos.DeleteProjectInfo;

/// <summary>
/// Represents a request to delete a project from a member profile.
/// </summary>
/// <param name="MemberProfileId">The ID of the member profile.</param>
/// <param name="ProjectInfoId">The ID of the project to delete.</param>
public record DeleteProjectInfoRequest(UserId MemberProfileId, ProjectInfoId ProjectInfoId) : IRequest<Result>;
