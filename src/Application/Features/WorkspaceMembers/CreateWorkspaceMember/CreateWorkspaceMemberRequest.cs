using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;

/// <summary>
/// Represents a request to create a workspace member.
/// </summary>
public record CreateWorkspaceMemberRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the workspace member invitations.
    /// </summary>
    public List<WorkspaceMemberInvitationRequest> WorkspaceMemberInvitations { get; init; } = [];
}

/// <summary>
/// Represents a request to create a workspace member invitation.
/// </summary>
public record WorkspaceMemberInvitationRequest
{
    /// <summary>
    /// Gets or sets the emails.
    /// </summary>
    public string Email { get; init; } = null!;

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public WorkspaceRole Role { get; init; }
}
