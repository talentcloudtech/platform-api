using System;
using System.Collections.Generic;
using Domain.Entities.Enums;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a workspace asset entity.
/// </summary>
public class WorkspaceAsset : AuditableEntity<WorkspaceAssetId>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the project ID (optional).
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the task ID (optional).
    /// </summary>
    public ProjectTaskId? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the file URL.
    /// </summary>
    public required string FileUrl { get; set; }

    /// <summary>
    /// Gets or sets the file MIME type.
    /// </summary>
    public required string FileType { get; set; }

    /// <summary>
    /// Gets or sets the file size in bytes.
    /// </summary>
    public required long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the scope of the asset.
    /// </summary>
    public required AssetScope Scope { get; set; }

    /// <summary>
    /// Gets or sets the project associated with this task.
    /// </summary>
    public Project Project { get; set; } = null!;

    /// <summary>
    /// Gets or sets the workspace associated with this task.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the task associated with this task.
    /// </summary>
    public ProjectTask Task { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this asset.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
