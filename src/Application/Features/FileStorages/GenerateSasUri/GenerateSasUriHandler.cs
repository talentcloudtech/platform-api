using Ardalis.Result;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using System.Net;

namespace Platform.Application.Features.FileStorages.GenerateSasUri;

/// <summary>
/// Handles the generation of a download URI for an uploaded file in Azure Blob Storage.
/// </summary>
public class GenerateDownloadUriHandler : IRequestHandler<GenerateSasUriRequest, Result<string>>
{
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateDownloadUriHandler"/> class.
    /// </summary>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage configuration options.</param>
    public GenerateDownloadUriHandler(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
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
    public async Task<Result<string>> Handle(GenerateSasUriRequest request, CancellationToken cancellationToken)
    {
        var filePathUrl = WebUtility.UrlDecode(request.FilePath);
        var filePathUri = new Uri(filePathUrl);
        var segments = filePathUri.AbsolutePath.Trim('/').Split('/');
        if (segments.Length < 2)
            return Result.Error("Cannot generate download Url");

        var containerName = segments[0];
        var resourcePath = segments[1];

        var builder = new BlobSasBuilder
        {
            Resource = "b",
            BlobContainerName = containerName,
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(24),
            BlobName = resourcePath
        };
        builder.SetPermissions(
            BlobAccountSasPermissions.Read | BlobAccountSasPermissions.Write | BlobAccountSasPermissions.Delete
        );

        var blobClient = new BlobClient(_azureBlobStorageOptions.ConnectionString, containerName, resourcePath);
        var uri = blobClient.GenerateSasUri(builder);

        return await Task.FromResult(Result.Success(uri.ToString())).ConfigureAwait(false);
    }
}
