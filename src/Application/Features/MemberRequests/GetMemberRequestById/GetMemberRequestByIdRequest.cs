using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberRequests.GetMemberRequestById;

/// <summary>
/// Represents a request to get a member request identified by its <see cref="MemberRequestId"/>.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace containing the member request.</param>
/// <param name="Id">The unique identifier of the member request to be retrieved.</param>
public record GetMemberRequestByIdRequest(WorkspaceId WorkspaceId, MemberRequestId Id) : IRequest<Result<MemberRequestResponse>>;
