using System.Collections.Generic;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a set of skills.
/// </summary>
public class Skill : Entity<SkillId>
{
    /// <summary>
    /// Gets or sets the unique identifier of the skill.
    /// </summary>
    public override SkillId Id { get; set; } = CustomNewId.GenerateStandardId();

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
