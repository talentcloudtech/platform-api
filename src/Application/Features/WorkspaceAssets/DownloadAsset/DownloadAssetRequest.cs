using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceAssets.DownloadAsset;

/// <summary>
/// Request to download a workspace asset
/// </summary>
public record DownloadAssetRequest : IRequest<Result<DownloadAssetResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier
    /// </summary>
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the asset identifier
    /// </summary>
    public WorkspaceAssetId AssetId { get; init; }
}
