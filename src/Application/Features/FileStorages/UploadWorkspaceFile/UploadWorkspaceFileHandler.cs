using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Services;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadWorkspaceFileHandler"/> class.
    /// </summary>
    /// <param name="blobStorageService">The blob storage service.</param>
    public UploadWorkspaceFileHandler(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
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
            var fileUrl = await _blobStorageService.UploadFileAsync(
                request.File,
                request.WorkspaceId,
                request.ProjectId,
                request.TaskId,
                cancellationToken
            );

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
}