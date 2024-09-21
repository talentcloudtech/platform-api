using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using Platform.Domain.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member of a workspace in the system.
/// </summary>
public class WorkspaceMember : Entity<WorkspaceMemberId>
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
    /// Gets or sets the date when the workspace member was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this workspace member.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the workspace member was last updated. Null if never updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this workspace member. Null if never updated.
    /// </summary>
    public UserId? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this workspace member.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this workspace member.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
