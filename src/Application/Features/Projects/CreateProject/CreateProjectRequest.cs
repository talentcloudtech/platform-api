using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Projects.CreateProject;

/// <summary>
/// Represents a request to create a project.
/// </summary>
public record CreateProjectRequest : IRequest<Result<ProjectResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string Title { get; init; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Gets or sets the budget for the project.
    /// </summary>
    public decimal? Budget { get; init; }
}
