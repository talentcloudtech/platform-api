using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.FileStorages.UploadWorkspaceFile;

/// <summary>
/// Represents a request to upload a file to the workspace.
/// </summary>
public record UploadWorkspaceFileRequest : IRequest<Result<UploadWorkspaceFileResponse>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project ID (optional).
    /// </summary>
    public ProjectId? ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the task ID (optional).
    /// </summary>
    public ProjectTaskId? TaskId { get; init; }

    /// <summary>
    /// Gets or sets the file to upload.
    /// </summary>
    public required IFormFile File { get; init; }
}
