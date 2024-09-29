using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.DeleteWorkspaceMemberInvitation;

/// <summary>
/// Request to delete a workspace member invitation.
/// </summary>
/// <param name="InvitationId">The invitation ID.</param>
/// <returns>The result of the operation.</returns>
public record DeleteWorkspaceMemberInvitationRequest(WorkspaceMemberInvitationId InvitationId) : IRequest<Result>;
