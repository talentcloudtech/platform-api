using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMembers.DeleteWorkspaceMember;

/// <summary>
/// Request to delete a workspace member.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace.</param>
/// <param name="UserId">The ID of the user.</param>
public record DeleteWorkspaceMemberRequest(WorkspaceId WorkspaceId, UserId UserId) : IRequest<Result>;
