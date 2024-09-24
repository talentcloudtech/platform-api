using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.AcceptInvitation;

/// <summary>
/// Represents a request to accept an invitation.
/// </summary>
public record AcceptInvitationRequest(string Code) : IRequest<Result<string>>
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the workspace member invitation id.
    /// </summary>
    public WorkspaceMemberInvitationId WorkspaceMemberInvitationId { get; set; }
}
