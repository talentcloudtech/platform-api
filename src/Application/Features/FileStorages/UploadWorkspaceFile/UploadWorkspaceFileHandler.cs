using Ardalis.Result;
using Domain.Entities.Enums;
using MediatR;
using Platform.Application.Common;
using Platform.Application.Common.Services;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Application.Features.FileStorages.UploadWorkspaceFile;

/// <summary>
/// Handles the process of uploading a file to the workspace asset container in Azure Blob Storage. 
/// </summary>
public class UploadWorkspaceFileHandler : IRequestHandler<UploadWorkspaceFileRequest, Result<UploadWorkspaceFileResponse>>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IContext _context;
    private readonly ICurrentSession _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadWorkspaceFileHandler"/> class.
    /// </summary>
    public UploadWorkspaceFileHandler(
        IBlobStorageService blobStorageService,
        IContext context,
        ICurrentSession currentUser)
    {
        _blobStorageService = blobStorageService;
        _context = context;
        _currentUser = currentUser;
    }

    /// <summary>
    /// Handles the process of uploading a file to the workspace asset container in Azure Blob Storage.
    /// </summary>
    /// <param name="request">The request containing the file to upload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns> 
    public async Task<Result<UploadWorkspaceFileResponse>> Handle(
        UploadWorkspaceFileRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Upload file to blob storage
            var fileUrl = await _blobStorageService.UploadFileAsync(
                request.File,
                request.WorkspaceId,
                request.ProjectId,
                request.TaskId,
                cancellationToken
            );

            // Determine the scope based on provided IDs
            var scope = DetermineScope(request.ProjectId, request.TaskId);

            // Create workspace asset entity
            var workspaceAsset = new WorkspaceAsset
            {
                WorkspaceId = request.WorkspaceId,
                ProjectId = request.ProjectId,
                TaskId = request.TaskId,
                FileUrl = fileUrl,
                FileType = request.File.ContentType,
                FileSize = request.File.Length,
                Scope = scope,
                CreatedBy = _currentUser.UserId,
                CreatedDate = DateTime.UtcNow,
                UpdatedBy = _currentUser.UserId,
                UpdatedDate = DateTime.UtcNow
            };

            // Save to database
            _context.WorkspaceAssets.Add(workspaceAsset);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(new UploadWorkspaceFileResponse
            {
                FileUrl = fileUrl,
                WorkspaceId = request.WorkspaceId
            });

        }
        catch (Exception ex)
        {
            return Result.Error($"Failed to upload file: {ex.Message}");
        }
    }

    private static AssetScope DetermineScope(ProjectId? projectId, ProjectTaskId? taskId)
    {
        if (taskId != null)
            return AssetScope.Task;

        if (projectId != null)
            return AssetScope.Project;

        return AssetScope.Workspace;
    }
}