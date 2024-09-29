using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.DeleteWorkspaceMemberInvitation;

/// <summary>
/// Request to delete a workspace member invitation.
/// </summary>
/// <param name="WorkspaceId">The workspace ID.</param>
/// <param name="InvitationId">The invitation ID.</param>
/// <returns>The result of the operation.</returns>
public record DeleteWorkspaceMemberInvitationRequest(WorkspaceId WorkspaceId, WorkspaceMemberInvitationId InvitationId) : IRequest<Result>;
