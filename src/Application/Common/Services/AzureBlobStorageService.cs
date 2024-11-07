using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Common.Services;

/// <summary>
/// Azure Blob Storage Service implementation.
/// </summary>
public class AzureBlobStorageService : IBlobStorageService
{
    private readonly AzureBlobStorageOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureBlobStorageService"/> class.
    /// </summary>
    /// <param name="options">The Azure Blob Storage configuration options. </param>
    public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Uploads a file to the Azure Blob Storage.
    /// </summary>
    /// <param name="file">The file to upload.</param>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="projectId">The project ID.</param>
    /// <param name="taskId">The task ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The URL of the uploaded file.</returns>
    public async Task<string> UploadFileAsync(
        IFormFile file,
        WorkspaceId workspaceId,
        ProjectId? projectId = null,
        ProjectTaskId? taskId = null,
        CancellationToken cancellationToken = default
    )
    {
        if (file == null || file.Length == 0)
            throw new InvalidOperationException("No file was provided or file is empty.");

        // Check file size (e.g., 100MB limit)
        const long maxFileSize = 100 * 1024 * 1024; // 100MB in bytes
        if (file.Length > maxFileSize)
            throw new InvalidOperationException("File size exceeds the maximum limit of 100MB.");

        var fileExt = Path.GetExtension(file.FileName);
        var fileName = $"{CustomNewId.GenerateShortId()}{fileExt}";
        var containerPath = BuildContainerPath(workspaceId, projectId, taskId);

        var containerClient = new BlobContainerClient(_options.ConnectionString, "workspaceassets");
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        var blobPath = $"{containerPath}/{fileName}";
        var blobClient = containerClient.GetBlobClient(blobPath);

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(
                stream,
                new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders { CacheControl = "public, max-age=31536000, immutable", ContentType = file.ContentType },
                },
                cancellationToken
            );

            return blobClient.Uri.ToString();
        }
    }

    /// <summary>
    /// Deletes a file from the Azure Blob Storage.
    /// </summary>
    /// <param name="fileUrl">The URL of the file to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileUrl))
            return;

        var uri = new Uri(fileUrl);
        var containerClient = new BlobContainerClient(_options.ConnectionString, "workspaceassets");
        var blobPath = uri.LocalPath.TrimStart('/').Split('/', 2)[1];
        var blobClient = containerClient.GetBlobClient(blobPath);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    private static string BuildContainerPath(WorkspaceId workspaceId, ProjectId? projectId = null, ProjectTaskId? taskId = null)
    {
        var path = $"{workspaceId}";

        if (projectId != null)
            path += $"/Projects/{projectId}";

        if (taskId != null)
            path += $"/Tasks/{taskId}";

        return path;
    }
}
