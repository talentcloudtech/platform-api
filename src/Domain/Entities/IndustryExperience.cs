namespace Platform.Domain.Entities;

/// <summary>
/// Represents an industry experience.
/// </summary>
public class IndustryExperience
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
