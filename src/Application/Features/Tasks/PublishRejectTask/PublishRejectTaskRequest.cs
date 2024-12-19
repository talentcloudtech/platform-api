using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.PublishRejectTask;

/// <summary>
/// Request to publish a task
/// </summary>
public record PublishRejectTaskRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project ID
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the task ID
    /// </summary>
    [JsonIgnore]
    public ProjectTaskId TaskId { get; init; }

    /// <summary>
    /// Gets or sets the reason for rejection
    /// </summary>
    public OpenTaskStatus? Status { get; init; }

    /// <summary>
    /// Gets or sets the reason for rejection
    /// </summary>
    public string? Reason { get; init; }
}
