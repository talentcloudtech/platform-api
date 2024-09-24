using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a project entity in the system.
/// </summary>
public class Project : AuditableEntity<ProjectId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the project.
    /// </summary>
    public override ProjectId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the workspace identifier associated with this project.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this project.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the budget for the project.
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Gets or sets the status of the project.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the collection of project members.
    /// </summary>
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();

    /// <summary>
    /// Gets or sets the collection of tasks associated with this project.
    /// </summary>
    public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    /// <summary>
    /// Gets or sets the user associated with this project.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
