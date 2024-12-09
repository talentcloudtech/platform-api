using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.LeaveWorkspace;

/// <summary>
/// Represents a request to leave a workspace.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace to be left.</param>
public record LeaveWorkspaceRequest(WorkspaceId WorkspaceId) : IRequest<Result>;
