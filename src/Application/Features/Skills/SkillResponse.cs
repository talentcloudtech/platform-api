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

    /// <summary>
    /// Gets or sets the key.
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the keywords.
    /// </summary>
    public string[]? Keywords { get; set; }
}
