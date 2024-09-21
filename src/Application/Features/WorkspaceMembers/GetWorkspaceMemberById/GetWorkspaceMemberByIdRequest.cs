using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMembers.GetWorkspaceMemberById;

/// <summary>
/// Represents a request to get a workspace member by their ID.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace.</param>
/// <param name="UserId">The ID of the user.</param>
public record GetWorkspaceMemberByIdRequest(WorkspaceId WorkspaceId, UserId UserId) : IRequest<Result<WorkspaceMemberResponse>>;
