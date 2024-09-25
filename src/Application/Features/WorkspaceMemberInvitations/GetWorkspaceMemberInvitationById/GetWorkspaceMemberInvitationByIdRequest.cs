using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.GetWorkspaceMemberInvitationById;

/// <summary>
/// Request to get a workspace member invitation by its ID.
/// </summary>
/// <param name="WorkspaceMemberInvitationId">The ID of the workspace member invitation.</param>
public record GetWorkspaceMemberInvitationByIdRequest(WorkspaceMemberInvitationId WorkspaceMemberInvitationId)
    : IRequest<Result<WorkspaceMemberInvitationResponse>>;
