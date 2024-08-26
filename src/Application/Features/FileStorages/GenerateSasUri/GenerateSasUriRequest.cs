using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.FileStorages.GenerateSasUri;

/// <summary>
/// Represents a request to generate a Shared Access Signature (SAS) URI for a blob in Azure Blob Storage.
/// </summary>
public record GenerateSasUriRequest(string FilePath) : IRequest<Result<string>>;
