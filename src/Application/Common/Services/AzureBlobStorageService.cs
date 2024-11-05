using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Domain.Entities.Common;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Platform.Application.Common.Services;

/// <summary>
/// Azure Blob Storage Service implementation.  
/// </summary>
public class AzureBlobStorageService : IBlobStorageService
{
    private readonly AzureBlobStorageOptions _options;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
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
    /// <exception cref="InvalidOperationException">Thrown when the file type is not allowed.</exception>
    public async Task<string> UploadFileAsync(
        IFormFile file,
        string workspaceId,
        string? projectId = null,
        string? taskId = null,
        CancellationToken cancellationToken = default)
    {
        var fileExt = Path.GetExtension(file.FileName)?.ToLowerInvariant();

        if (!Array.Exists(AllowedExtensions, ext => string.Equals(ext, fileExt, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Invalid file type. Only image files (.jpg, .jpeg, .png, .gif) and PDFs (.pdf) are allowed.");

        var fileName = $"{CustomNewId.GenerateShortId()}{fileExt}";
        var containerPath = BuildContainerPath(workspaceId, projectId, taskId);

        var containerClient = new BlobContainerClient(_options.ConnectionString, "workspaceassets");
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        var blobPath = $"{containerPath}/{fileName}";
        var blobClient = containerClient.GetBlobClient(blobPath);

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    CacheControl = "public, max-age=31536000, immutable",
                    ContentType = FileHelper.GetContentType(fileName)
                }
            }, cancellationToken);
        }

        return blobClient.Uri.ToString();
    }

    /// <summary>
    /// Deletes a file from the Azure Blob Storage. 
    /// </summary>
    /// <param name="fileUrl">The URL of the file to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteFileAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(fileUrl)) return;

        var uri = new Uri(fileUrl);
        var containerClient = new BlobContainerClient(_options.ConnectionString, "WorkspaceAssets");
        var blobPath = uri.LocalPath.TrimStart('/').Split('/', 2)[1];
        var blobClient = containerClient.GetBlobClient(blobPath);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    private static string BuildContainerPath(string workspaceId, string? projectId = null, string? taskId = null)
    {
        var path = $"WorkspaceIds/{workspaceId}";

        if (!string.IsNullOrEmpty(projectId))
            path += $"/ProjectIds/{projectId}";

        if (!string.IsNullOrEmpty(taskId))
            path += $"/TaskIds/{taskId}";

        return path;
    }
}