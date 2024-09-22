using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectMembers.GetProjectMemberById;

/// <summary>
/// Request to get a project member by their ID.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace.</param>
/// <param name="ProjectId">The ID of the project.</param>
/// <param name="UserId">The ID of the user.</param>
public record GetProjectMemberByIdRequest(WorkspaceId WorkspaceId, ProjectId ProjectId, UserId UserId) : IRequest<Result<ProjectMemberResponse>>;
