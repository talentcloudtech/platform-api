using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.GetUserProjectRole;

/// <summary>
/// Request to get the role of a user in a project.
/// </summary>
/// <param name="ProjectId">The unique identifier of the project.</param>
public record GetUserProjectRoleRequest(ProjectId ProjectId) : IRequest<Result<ProjectMemberRoleResponse>>;
