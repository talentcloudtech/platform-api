using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.GetUserWorkspaceRole;

/// <summary>
/// Request to get the role of a user in a workspace.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace.</param>
public record GetUserWorkspaceRoleRequest(WorkspaceId WorkspaceId) : IRequest<Result<WorkspaceMemberRoleResponse>>;
