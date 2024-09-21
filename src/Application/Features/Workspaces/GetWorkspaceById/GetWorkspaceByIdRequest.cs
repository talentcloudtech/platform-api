using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.GetWorkspaceById;

/// <summary>
/// Represents a request to get a workspace identified by its <see cref="WorkspaceId"/>.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace to be retrieved.</param>
public record GetWorkspaceByIdRequest(WorkspaceId WorkspaceId) : IRequest<Result<WorkspaceResponse>>;
