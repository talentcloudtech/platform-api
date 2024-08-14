namespace Platform.Domain.Entities;

/// <summary>
/// Represents a set of skills.
/// </summary>
public class Skill
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the experience year.
    /// </summary>
    public int ExperienceYear { get; set; }
}
