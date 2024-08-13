using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Projects.CreateProject;

/// <summary>
/// Represents a request to create a project.
/// </summary>
public record CreateProjectRequest : IRequest<Result<ProjectResponse>>
{
    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string? Description { get; init; }
}
