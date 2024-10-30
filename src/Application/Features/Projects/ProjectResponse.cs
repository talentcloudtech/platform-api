using System;
using System.Collections;
using System.Collections.Generic;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities;
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
    /// Gets or sets the workspace identifier.
    /// </summary>
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
    public DateTime? StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime? EndDate { get; init; }

    /// <summary>
    /// Gets or sets the budget for the project.
    /// </summary>
    public decimal? Budget { get; init; }

    /// <summary>
    /// Gets the current status of the project.
    /// </summary>
    public ProjectStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this project member.
    /// </summary>
    public UserId CreatedBy { get; init; }

    /// <summary>
    /// Gets or sets the date when the project member was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; init; }

    /// <summary>
    /// Gets or  the identifier of the user who last updated this project member.
    /// </summary>
    public UserId? UpdatedBy { get; init; }

    /// <summary>
    /// Gets or sets the workspace.
    /// </summary>
    public WorkspaceResponse? Workspace { get; set; }

    /// <summary>
    /// Gets or sets the created by user.
    /// </summary>
    public CommonUserResponse? CreatedByUser { get; set; }

    /// <summary>
    /// Gets or sets the project technologies.
    /// </summary>
    public IList<ProjectTechnology> Technologies { get; set; } = new List<ProjectTechnology>();

    /// <summary>
    /// Gets or sets the type of the project.
    /// </summary>
    public ProjectType Type { get; init; }
}
