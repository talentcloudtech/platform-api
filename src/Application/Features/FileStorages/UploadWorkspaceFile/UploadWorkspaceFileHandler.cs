using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Application.Common.Services;
using Platform.Application.Features.WorkspaceAssets;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.FileStorages.UploadWorkspaceFile;

/// <summary>
/// Handles the process of uploading a file to the workspace asset container in Azure Blob Storage.
/// </summary>
public class UploadWorkspaceFileHandler : IRequestHandler<UploadWorkspaceFileRequest, Result<WorkspaceAssetResponse>>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IContext _context;
    private readonly ICurrentSession _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadWorkspaceFileHandler"/> class.
    /// </summary>
    public UploadWorkspaceFileHandler(IBlobStorageService blobStorageService, IContext context, ICurrentSession currentUser)
    {
        _blobStorageService = blobStorageService;
        _context = context;
        _currentUser = currentUser;
    }

    private static AssetScope DetermineScope(ProjectId? projectId, ProjectTaskId? taskId)
    {
        if (taskId != null)
            return AssetScope.Task;

        if (projectId != null)
            return AssetScope.Project;

        return AssetScope.Workspace;
    }

    /// <summary>
    /// Handles the process of uploading a file to the workspace asset container in Azure Blob Storage.
    /// </summary>
    public async Task<Result<WorkspaceAssetResponse>> Handle(UploadWorkspaceFileRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Upload file to blob storage
            var fileUrl = await _blobStorageService.UploadFileAsync(request.File, request.WorkspaceId, request.ProjectId, request.TaskId, cancellationToken);

            var scope = DetermineScope(request.ProjectId, request.TaskId);
            var currentTime = DateTime.UtcNow;

            var workspaceAsset = new WorkspaceAsset
            {
                WorkspaceId = request.WorkspaceId,
                ProjectId = request.ProjectId,
                TaskId = request.TaskId,
                FileName = request.File.FileName,
                FileUrl = fileUrl,
                FileType = request.File.ContentType,
                FileSize = request.File.Length,
                Scope = scope,
                CreatedBy = _currentUser.UserId,
                CreatedDate = currentTime,
                UpdatedBy = _currentUser.UserId,
                UpdatedDate = currentTime,
            };

            _context.WorkspaceAssets.Add(workspaceAsset);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(workspaceAsset.Adapt<WorkspaceAssetResponse>());
        }
        catch (Exception ex)
        {
            return Result.Error($"Failed to upload file: {ex.Message}");
        }
    }
}
