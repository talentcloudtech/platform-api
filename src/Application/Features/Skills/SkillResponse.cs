using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Skills;

/// <summary>
/// Represents a skill response.
/// </summary>
public record SkillResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the skill.
    /// </summary>
    public SkillId Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public required string Name { get; set; }
}
