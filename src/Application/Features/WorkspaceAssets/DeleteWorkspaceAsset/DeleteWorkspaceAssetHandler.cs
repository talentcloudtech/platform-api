using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Services;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceAssets.DeleteWorkspaceAsset;

/// <summary>
/// Handles the deletion of workspace assets
/// </summary>
public class DeleteWorkspaceAssetHandler : IRequestHandler<DeleteWorkspaceAssetRequest, Result>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;
    private readonly IBlobStorageService _blobStorageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteWorkspaceAssetHandler"/> class
    /// </summary>
    public DeleteWorkspaceAssetHandler(IContext context, ICurrentSession session, IBlobStorageService blobStorageService)
    {
        _context = context;
        _session = session;
        _blobStorageService = blobStorageService;
    }

    /// <summary>
    /// Handles the deletion request
    /// </summary>
    public async Task<Result> Handle(DeleteWorkspaceAssetRequest request, CancellationToken cancellationToken)
    {
        var workspace = await _context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        var member = workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == _session.UserId);

        var asset = await _context.WorkspaceAssets.FirstOrDefaultAsync(x => x.Id == request.AssetId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (asset == null)
            return Result.NotFound("Asset not found.");

        try
        {
            if (asset.FileType != "link")
            {
                await _blobStorageService.DeleteFileAsync(asset.FileUrl, cancellationToken);
            }

            _context.WorkspaceAssets.Remove(asset);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error($"Failed to delete asset: {ex.Message}");
        }
    }
}
