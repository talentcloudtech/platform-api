using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

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
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the budget for the project.
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Gets the current status of the project.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the type of the project.
    /// </summary>
    public ProjectType Type { get; set; }

    /// <summary>
    /// Gets or sets the technologies.
    /// </summary>
    public IList<ProjectTechnology> Technologies { get; set; } = new List<ProjectTechnology>();
}
