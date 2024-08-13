using MassTransit;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Domain.Entities;

/// <summary>
/// /Represents a project entity.
/// </summary>
public class Project : Entity<ProjectId>
{
    /// <summary>
    /// Gets or sets the unique identifier of the project.
    /// </summary>
    public override ProjectId Id { get; set; } = NewId.NextGuid();

    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the status of the project.
    /// </summary>
    public ProjectStatus Status { get; init; }

    // /// <summary>
    // /// Gets the list of project users.
    // /// </summary>
    // public List<ProjectUser> ProjectUsers { get; } = [];
}
