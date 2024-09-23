using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member of a workspace in the system.
/// </summary>
public class WorkspaceMember : AuditableEntity<WorkspaceMemberId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace member.
    /// </summary>
    public override WorkspaceMemberId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the user identifier associated with this workspace member.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this workspace member.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the role of the member in the workspace.
    /// </summary>
    public WorkspaceRole Role { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this workspace member.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this workspace member.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
