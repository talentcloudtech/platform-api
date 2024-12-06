using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Application.Common.Services;
using Platform.Application.Features.FileStorages.UploadFile;
using Platform.Application.Features.WorkspaceAssets;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.FileStorages.UploadFileLink;

/// <summary>
/// Handles the process of uploading a file link to the workspace asset container.
/// </summary>
public class UploadFileLinkHandler : IRequestHandler<UploadFileLinkRequest, Result<WorkspaceAssetResponse>>
{
    /// <summary>
    /// The database context.
    /// </summary>
    private readonly IContext _context;

    /// <summary>
    /// The current user session.
    /// </summary>
    private readonly ICurrentSession _currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadFileLinkHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="currentUser">The current user session.</param>
    public UploadFileLinkHandler(IContext context, ICurrentSession currentUser)
    {
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
    /// Handles the process of uploading a file link to the workspace asset container.
    /// </summary>
    public async Task<Result<WorkspaceAssetResponse>> Handle(UploadFileLinkRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var scope = DetermineScope(request.ProjectId, request.TaskId);
            var currentTime = DateTime.UtcNow;

            var workspaceAsset = new WorkspaceAsset
            {
                WorkspaceId = request.WorkspaceId,
                ProjectId = request.ProjectId,
                TaskId = request.TaskId,
                FileName = request.Title,
                FileUrl = request.Url,
                FileType = "link",
                FileSize = 0,
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
