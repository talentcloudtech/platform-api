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
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
