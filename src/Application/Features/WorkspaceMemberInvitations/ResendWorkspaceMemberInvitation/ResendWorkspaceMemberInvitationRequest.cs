using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.ResendWorkspaceMemberInvitation;

/// <summary>
/// Request to resend a workspace member invitation.
/// </summary>
public record ResendWorkspaceMemberInvitationRequest : IRequest<Result>
{
    /// <summary>
    /// The ID of the invitation to resend.
    /// </summary>
    [JsonIgnore]
    public WorkspaceMemberInvitationId InvitationId { get; init; }
}
