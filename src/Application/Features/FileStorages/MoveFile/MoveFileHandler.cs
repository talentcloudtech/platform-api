using Ardalis.Result;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;

namespace Platform.Application.Features.FileStorages.MoveFile;

/// <summary>
/// Handles the request to move a file from the temporary container to the actual container in Azure Blob Storage.
/// </summary>
public class MoveFileHandler : IRequestHandler<MoveFileRequest, Result<string>>
{
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveFileHandler"/> class.
    /// </summary>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage options.</param>
    public MoveFileHandler(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the request to move a file from the temporary container to the actual container.
    /// </summary>
    /// <param name="request">The request containing the download URI, temp path, and actual path of the file to be moved.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="Result{T}"/> containing the URL of the moved file in the actual container.</returns>
    public async Task<Result<string>> Handle(MoveFileRequest request, CancellationToken cancellationToken)
    {
        var fileName = new Uri(request.DownloadUri).Segments.Last();

        var actualBlobContainerClient = new BlobContainerClient(
            _azureBlobStorageOptions.ConnectionString,
            request.ActualContainerPath
        );
        await actualBlobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None);

        var tempBlobClient = new BlobClient(
            _azureBlobStorageOptions.ConnectionString,
            request.TempContainerPath,
            fileName
        );
        var destinationBlobClient = actualBlobContainerClient.GetBlobClient(fileName);
        await destinationBlobClient.StartCopyFromUriAsync(tempBlobClient.Uri);

        await tempBlobClient.DeleteAsync();

        var blobUrl = destinationBlobClient.Uri.ToString();

        return blobUrl;
    }
}
