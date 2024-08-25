using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.FileStorages.DeleteFile;

/// <summary>
/// Represents a request to delete a file identified by its file path.
/// </summary>
/// <param name="FilePath">The path of the file to be deleted.</param>
public record DeleteFileRequest(string FilePath) : IRequest<Result<string>>;
