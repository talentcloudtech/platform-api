using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Services;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.WorkspaceAssets.DownloadAsset;

/// <summary>
/// Handler for downloading workspace assets
/// </summary>
public class DownloadAssetHandler : IRequestHandler<DownloadAssetRequest, Result<DownloadAssetResponse>>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;
    private readonly IBlobStorageService _blobStorage;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadAssetHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="session">The current user session.</param>
    /// <param name="blobStorage">The blob storage service.</param>
    public DownloadAssetHandler(IContext context, ICurrentSession session, IBlobStorageService blobStorage)
    {
        _context = context;
        _session = session;
        _blobStorage = blobStorage;
    }

    /// <summary>
    /// Handles the download request
    /// </summary>
    public async Task<Result<DownloadAssetResponse>> Handle(DownloadAssetRequest request, CancellationToken cancellationToken)
    {
        // Verify workspace membership
        var workspaceMember = await _context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == _session.UserId,
            cancellationToken
        );

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");

        // Get asset details
        var asset = await _context.WorkspaceAssets.FirstOrDefaultAsync(a => a.Id == request.AssetId && a.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (asset == null)
            return Result.NotFound("Asset not found.");

        // Download file from blob storage
        var presignedUrl = await _blobStorage.GenerateSasUrl(asset.FileUrl, request.IsPreview ? null : asset.FileName, cancellationToken);

        if (presignedUrl == null)
            return Result.Error("Failed to download asset.");

        return Result.Success(
            new DownloadAssetResponse
            {
                PresignedUrl = presignedUrl,
                ContentType = asset.FileType,
                FileName = asset.FileName,
            }
        );
    }
}
