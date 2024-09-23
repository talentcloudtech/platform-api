using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectMembers.DeleteProjectMember;

/// <summary>
/// Request to delete a project member.
/// </summary>
/// <param name="WorkspaceId">The workspace ID.</param>
/// <param name="ProjectId">The project ID.</param>
/// <param name="UserId">The user ID.</param>

public record DeleteProjectMemberRequest(WorkspaceId WorkspaceId, ProjectId ProjectId, UserId UserId) : IRequest<Result>;
