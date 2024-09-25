using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents information about a project.
/// </summary>
public class ProjectInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of the project entry.
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
    [NotMapped]
    public ShortSkillResponse[]? Technologies { get; set; }
}
