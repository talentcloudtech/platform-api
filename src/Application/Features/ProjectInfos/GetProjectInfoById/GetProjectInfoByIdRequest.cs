using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectInfos.GetProjectInfoById;

/// <summary>
/// Represents a request to retrieve a project by its ID for a specific member profile.
/// </summary>
public record GetProjectInfoByIdRequest(UserId MemberProfileId, string ProjectInfoId) : IRequest<Result<ProjectInfoResponse>>;
