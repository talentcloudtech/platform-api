using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an invitation to join a workspace.
/// </summary>
public class WorkspaceMemberInvitation : AuditableEntity<WorkspaceMemberInvitationId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace member invitation.
    /// </summary>
    public override WorkspaceMemberInvitationId Id { get; set; } = CustomNewId.GenerateStandardId();

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
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this workspace member invitation.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
