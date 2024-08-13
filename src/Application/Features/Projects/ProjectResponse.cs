using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects;

/// <summary>
/// Represents the response model for a project in the application.
/// </summary>
public record ProjectResponse
{
    /// <summary>
    /// Gets the unique identifier for the project.
    /// </summary>
    public ProjectId Id { get; init; }

    /// <summary>
    /// Gets the name of the project.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets or sets the description of the project. Can be null.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets the current status of the project.
    /// </summary>
    public ProjectStatus Status { get; init; }
}
