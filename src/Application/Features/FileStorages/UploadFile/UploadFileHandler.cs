using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Features.FileStorages.GenerateSasUri;

namespace Platform.Application.Features.FileStorages.UploadFile;

/// <summary>
/// Handles the generation of a download URI for an uploaded file in Azure Blob Storage.
/// </summary>
public class UploadFileHandler : IRequestHandler<UploadFileRequest, Result<string>>
{
    private readonly IMediator _mediator;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateDownloadUriHandler"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage configuration options.</param>
    public UploadFileHandler(IMediator mediator, IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        _mediator = mediator;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the process of uploading a file to a temporary container in Azure Blob Storage and generating a download URI.
    /// </summary>
    /// <param name="request">The request containing the file to be uploaded.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the download URI as a string if successful,
    /// or an error message if the operation fails.
    /// </returns>
    public async Task<Result<string>> Handle(UploadFileRequest request, CancellationToken cancellationToken)
    {
        var fileExt = Path.GetExtension(request.File.FileName)?.ToLowerInvariant();

        if (!Array.Exists(AllowedExtensions, ext => string.Equals(ext, fileExt, StringComparison.OrdinalIgnoreCase)))
            return Result.Error(
                "Invalid file type. Only image files (.jpg, .jpeg, .png, .gif) and PDFs (.pdf) are allowed."
            );

        var fileName = Guid.NewGuid() + fileExt;

        var tempBlobContainerClient = new BlobContainerClient(
            _azureBlobStorageOptions.ConnectionString,
            _azureBlobStorageOptions.TempContainer
        );
        await tempBlobContainerClient.CreateIfNotExistsAsync(PublicAccessType.None);

        var tempBlobClient = tempBlobContainerClient.GetBlobClient(fileName);
        await using (var stream = request.File.OpenReadStream())
        {
            await tempBlobClient.UploadAsync(stream, cancellationToken);
        }
        var result = await _mediator.Send(new GenerateSasUriRequest(tempBlobClient.Uri.ToString()), cancellationToken);

        return Result.Success(result);
    }
}
