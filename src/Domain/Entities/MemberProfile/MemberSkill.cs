using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a skill that a member has.
/// </summary>
public class MemberSkill
{
    /// <summary>
    /// Gets or sets the unique identifier of the language entry.
    /// </summary>
    public required string Identifier { get; set; }

    /// <summary>
    /// Gets or sets the skill identifier.
    /// </summary>
    public required SkillId SkillId { get; set; }

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
