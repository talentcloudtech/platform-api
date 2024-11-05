using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.FileStorages.UploadWorkspaceFile;

/// <summary>
/// Represents a request to upload a file to the workspace asset container in Azure Blob Storage.   
/// </summary>
public record UploadWorkspaceFileRequest : IRequest<Result<UploadWorkspaceFileResponse>>
{
    /// <summary>
    /// The ID of the workspace.    
    /// </summary>
    public required WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// The ID of the project.
    /// </summary>
    public ProjectId? ProjectId { get; init; }

    /// <summary>
    /// The ID of the task.
    /// </summary>
    public ProjectTaskId? TaskId { get; init; }

    /// <summary>
    /// The file to upload.
    /// </summary>
    public required IFormFile File { get; init; }
}

/// <summary>
/// Represents a response containing the URL of the uploaded file.
/// </summary>
public record UploadWorkspaceFileResponse
{
    /// <summary>
    /// The URL of the uploaded file.
    /// </summary>
    public required string FileUrl { get; init; }

    /// <summary>
    /// The workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; set; }
}