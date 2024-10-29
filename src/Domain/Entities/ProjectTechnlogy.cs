using System.ComponentModel.DataAnnotations.Schema;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a technology that a project requires.
/// </summary>
public class ProjectTechnology
{
    /// <summary>
    /// Gets or sets the technology that the project requires.
    /// </summary>
    public required SkillId TechnologyId { get; set; }

    /// <summary>
    /// Gets or sets the name of the technology.
    /// </summary>
    public required string Name { get; set; }
}
