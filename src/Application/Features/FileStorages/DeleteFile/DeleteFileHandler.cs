using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure.Storage.Blobs;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;

namespace Platform.Application.Features.FileStorages.DeleteFile;

/// <summary>
/// Handles the deletion of a file from Azure Blob Storage.
/// </summary>
public class DeleteFileHandler : IRequestHandler<DeleteFileRequest, Result<string>>
{
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFileHandler"/> class.
    /// </summary>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage configuration options.</param>
    public DeleteFileHandler(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the deletion of a file from Azure Blob Storage.
    /// </summary>
    /// <param name="request">The request containing the file path to be deleted.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a success message if the deletion is successful,
    /// or an error message if the operation fails.
    /// </returns>
    public async Task<Result<string>> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
    {
        var filePathUrl = WebUtility.UrlDecode(request.FilePath);
        var filePathUri = new Uri(filePathUrl);
        var segments = filePathUri.AbsolutePath.Trim('/').Split('/');
        if (segments.Length < 2)
            return Result.Error("Cannot generate download Url");

        var containerName = segments[0];
        var blobName = segments[1];

        var blobContainerClient = new BlobContainerClient(_azureBlobStorageOptions.ConnectionString, containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        return Result.Success();
    }
}
