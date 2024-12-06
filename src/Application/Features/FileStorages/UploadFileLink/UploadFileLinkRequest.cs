using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platform.Application.Features.WorkspaceAssets;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.FileStorages.UploadFileLink;

/// <summary>
/// Represents a request to upload a file.
/// </summary>
public record UploadFileLinkRequest : IRequest<Result<WorkspaceAssetResponse>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project ID (optional).
    /// </summary>
    [JsonIgnore]
    public ProjectId? ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the task ID (optional).
    /// </summary>
    [JsonIgnore]
    public ProjectTaskId? TaskId { get; init; }

    /// <summary>
    /// Gets or sets the file name.
    /// This property is required and must be provided as part of the form data.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the file to be uploaded.
    /// This property is required and must be provided as part of the form data.
    /// </summary>
    public required string Url { get; set; }
}
