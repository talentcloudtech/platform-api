using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a workspace entity in the system.
/// </summary>

public class Workspace : Entity<WorkspaceId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace.
    /// </summary>
    public override WorkspaceId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the name of the workspace.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the contact email for the workspace.
    /// </summary>
    public string ContactEmail { get; set; } = null!;

    /// <summary>
    /// Gets or sets the URLs associated with the workspace.
    /// </summary>
    public List<string> Urls { get; set; } = new();

    /// <summary>
    /// Gets or sets the address for the workspace.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the logo for the workspace.
    /// </summary>
    public string? Logo { get; set; }

    /// <summary>
    /// Gets or sets the date when the workspace was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created the workspace.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the workspace was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who last updated the workspace.
    /// </summary>
    public UserId? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the members of the workspace.
    /// </summary>
    public ICollection<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();
}
