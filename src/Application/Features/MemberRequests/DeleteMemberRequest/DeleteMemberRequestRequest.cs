using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberRequests.DeleteMemberRequest;

/// <summary>
/// Represents a request to delete a member request identified by its <see cref="MemberRequestId"/>.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace.</param>
/// <param name="Id">The unique identifier of the member request to be deleted.</param>
public record DeleteMemberRequestRequest(WorkspaceId WorkspaceId, MemberRequestId Id) : IRequest<Result>;
