using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Platform.Application.Features.FileStorages.UploadFile;

/// <summary>
/// Represents a request to upload a file.
/// </summary>
public record UploadFileRequest : IRequest<Result<string>>
{
    /// <summary>
    /// Gets or sets the file to be uploaded.
    /// This property is required and must be provided as part of the form data.
    /// </summary>
    [FromForm]
    public required IFormFile File { get; set; }
}
