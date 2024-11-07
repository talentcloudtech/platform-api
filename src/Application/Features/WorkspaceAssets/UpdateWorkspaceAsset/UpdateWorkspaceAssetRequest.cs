using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Collections.Generic;

namespace Platform.Application.Features.WorkspaceAssets.UpdateWorkspaceAsset;

/// <summary>
/// Request to update a workspace asset
/// </summary>
public record UpdateWorkspaceAssetRequest : IRequest<Result<WorkspaceAssetResponse>>
{
    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the asset ID to update
    /// </summary>
    public WorkspaceAssetId AssetId { get; init; }

    /// <summary>
    /// Gets or sets the new file name
    /// </summary>
    public string? FileName { get; init; }

    /// <summary>
    /// Gets or sets the list of fields to update
    /// </summary>
    public required ICollection<string> FieldsToUpdate { get; init; }
}