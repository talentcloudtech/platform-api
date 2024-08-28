using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.FileStorages.MoveFile;

/// <summary>
/// Represents a request to move a file from a temporary container to an actual container in Azure Blob Storage.
/// </summary>
public class MoveFileRequest : IRequest<Result<string>>
{
    /// <summary>
    /// Gets or sets the download URI of the file to be moved.
    /// </summary>
    public required string DownloadUri { get; set; }

    /// <summary>
    /// Gets or sets the temporary container path where the file is currently stored.
    /// </summary>
    public required string TempContainerPath { get; set; }

    /// <summary>
    /// Gets or sets the actual container path where the file should be moved.
    /// </summary>
    public required string ActualContainerPath { get; set; }
}
