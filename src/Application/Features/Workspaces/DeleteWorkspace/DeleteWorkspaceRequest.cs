using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.DeleteWorkspace;

/// <summary>
/// Represents a request to delete a workspace.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace to be deleted.</param>
public record DeleteWorkspaceRequest(WorkspaceId WorkspaceId) : IRequest<Result>;
