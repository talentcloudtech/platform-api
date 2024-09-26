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
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string? Name { get; set; }
}
