using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
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

        var containerClient = new BlobContainerClient(_options.ConnectionString, _options.WorkspaceAssets);
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
        var containerClient = new BlobContainerClient(_options.ConnectionString, _options.WorkspaceAssets);
        var blobPath = uri.LocalPath.TrimStart('/').Split('/', 2)[1];
        var blobClient = containerClient.GetBlobClient(blobPath);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Builds the container path for the Azure Blob Storage.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="projectId">The project ID.</param>
    /// <param name="taskId">The task ID.</param>
    /// <returns>The container path.</returns>
    private static string BuildContainerPath(WorkspaceId workspaceId, ProjectId? projectId = null, ProjectTaskId? taskId = null)
    {
        var path = $"{workspaceId}";

        if (projectId != null)
            path += $"/projects/{projectId}";

        if (taskId != null)
            path += $"/tasks/{taskId}";

        return path;
    }

    /// <summary>
    /// Downloads a file from the Azure Blob Storage.
    /// </summary>
    /// <param name="filePath">The path to the file in the Blob Storage.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The downloaded file as a byte array.</returns>
    public async Task<byte[]?> DownloadAsync(string filePath, CancellationToken cancellationToken)
    {
        try
        {
            // Extract the blob path from the full URL if a complete URL is provided
            var blobPath = ExtractBlobPath(filePath);

            var containerClient = new BlobContainerClient(_options.ConnectionString, _options.WorkspaceAssets);
            var blobClient = containerClient.GetBlobClient(blobPath);

            if (!await blobClient.ExistsAsync(cancellationToken))
                return null;

            var response = await blobClient.DownloadContentAsync(cancellationToken);
            return response.Value.Content.ToArray();
        }
        catch (Exception ex)
        {
            // Log the error details
            throw new InvalidOperationException($"Failed to download file: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Generate the sas token for presigned URL
    /// </summary>
    /// <param name="filePath">The file path</param>
    /// <param name="fileName">The file name if it needs to be downloaded.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The downloaded file as a byte array.</returns>
    public async Task<string?> GenerateSasUrl(string filePath, string? fileName, CancellationToken cancellationToken)
    {
        try
        {
            // Extract the blob path from the full URL if a complete URL is provided
            var blobPath = ExtractBlobPath(filePath);
            var containerName = _options.WorkspaceAssets;

            var blobClient = new BlobClient(_options.ConnectionString, containerName, blobPath);

            if (!await blobClient.ExistsAsync(cancellationToken))
                return null;

            var builder = new BlobSasBuilder
            {
                Resource = "b",
                BlobContainerName = containerName,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(2),
                BlobName = blobPath,
            };

            builder.SetPermissions(BlobAccountSasPermissions.Read);
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                builder.ContentDisposition = $"attachment; filename=\"{fileName}\"";
            }

            var uri = blobClient.GenerateSasUri(builder);
            return uri.ToString();
        }
        catch (Exception ex)
        {
            // Log the error details
            throw new InvalidOperationException($"Failed to download file: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Extract the blob path from the file path
    /// </summary>
    /// <param name="filePath">The file path</param>
    /// <returns>The blob path</returns>
    /// <exception cref="InvalidOperationException"></exception>
    private string ExtractBlobPath(string filePath)
    {
        var blobPath = filePath;
        if (Uri.TryCreate(filePath, UriKind.Absolute, out var uri))
        {
            // Split the path and get everything after the container name
            var pathParts = uri.LocalPath.TrimStart('/').Split('/', 2);
            if (pathParts.Length == 2)
                blobPath = pathParts[1];
        }

        return blobPath;
    }
}
