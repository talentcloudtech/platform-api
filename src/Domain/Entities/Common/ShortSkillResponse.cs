using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities.Common;

/// <summary>
/// Represents a skill response.
/// </summary>
public record ShortSkillResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the skill.
    /// </summary>
    public SkillId Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }
}
