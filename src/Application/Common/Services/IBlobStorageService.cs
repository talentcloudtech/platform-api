using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Common.Services;

/// <summary>
/// Represents a service for managing file storage operations in Azure Blob Storage.
/// </summary>
public interface IBlobStorageService
{
    /// <summary>
    /// Uploads a file to the Azure Blob Storage.
    /// </summary>
    /// <param name="file">The file to upload.</param>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="projectId">The project ID.</param>
    /// <param name="taskId">The task ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The URL of the uploaded file.</returns>
    Task<string> UploadFileAsync(
        IFormFile file,
        WorkspaceId workspaceId,
        ProjectId? projectId = null,
        ProjectTaskId? taskId = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Deletes a file from the Azure Blob Storage.
    /// </summary>
    /// <param name="fileUrl">The URL of the file to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default);
}
