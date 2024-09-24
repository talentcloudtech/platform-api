using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMemberInvitations;

/// <summary>
/// Represents a workspace member invitation response.
/// </summary>
public class WorkspaceMemberInvitationResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace member invitation.
    /// </summary>
    public WorkspaceMemberInvitationId Id { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this workspace member invitation.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user invited to the workspace.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the role of the member in the workspace.
    /// </summary>
    public required WorkspaceRole Role { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this workspace member invitation.
    /// </summary>
    public WorkspaceResponse Workspace { get; set; } = null!;
}
