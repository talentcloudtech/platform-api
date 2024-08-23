using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.Projects.UpdateProject;

/// <summary>
/// Represents a request to update a project's details.
/// </summary>
public record UpdateProjectRequest : IRequest<Result<ProjectResponse>>
{
    /// <summary>
    /// Gets the unique identifier of the project to be updated.
    /// </summary>
    [JsonIgnore]
    public ProjectId Id { get; init; }

    /// <summary>
    /// Gets the name of the project.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets the status of the project.
    /// </summary>
    public ProjectStatus Status { get; init; }
}
