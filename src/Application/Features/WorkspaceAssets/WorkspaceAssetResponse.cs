using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceAssets;

/// <summary>
/// Response model for workspace assets
/// </summary>
public class WorkspaceAssetResponse
{
    /// <summary>
    /// Gets or sets the asset ID
    /// </summary>
    public required WorkspaceAssetId Id { get; init; }

    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the project ID
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the task ID
    /// </summary>
    public ProjectTaskId? TaskId { get; set; }

    /// <summary>
    /// Gets or sets the file URL
    /// </summary>
    public required string FileUrl { get; set; }

    /// <summary>
    /// Gets or sets the file type
    /// </summary>
    public required string FileType { get; set; }

    /// <summary>
    /// Gets or sets the file size
    /// </summary>
    public required long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the asset scope
    /// Gets or sets the asset scope
    /// </summary>
    public required AssetScope Scope { get; set; }

    /// <summary>
    /// Gets or sets the workspace name
    /// </summary>
    public required string WorkspaceName { get; set; }

    /// <summary>
    /// Gets or sets the project name
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// Gets or sets the task name
    /// </summary>
    public string? TaskName { get; set; }

    /// <summary>
    /// Gets or sets the file name
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets who created the asset
    /// </summary>
    public required UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets when the asset was created
    /// </summary>
    public required DateTime CreatedDate { get; set; }
}
