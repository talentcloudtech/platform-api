using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.UpdateWorkspaceMemberInvitation;

/// <summary>
/// Request to update a workspace member invitation.
/// </summary>
public record UpdateWorkspaceMemberInvitationRequest : IRequest<Result<string>>
{
    /// <summary>
    /// The workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// The workspace member invitation ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceMemberInvitationId WorkspaceMemberInvitationId { get; init; }

    /// <summary>
    /// The code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Whether to accept the invitation.
    /// </summary>
    public bool Accept { get; init; }
}
