using System.Collections.Generic;
using Platform.Application.Features.Skills;
using Platform.Domain.Entities.Common;

/// <summary>
/// Represents a response for an experience.
/// </summary>
public class ProjectInfoResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the experience.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the link to the project.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets a short description of the project.
    /// </summary>
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets a list of technologies used in the project.
    /// </summary>
    public List<ShortSkillResponse>? Technologies { get; set; }
}
