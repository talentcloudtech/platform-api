using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a skill that a member has.
/// </summary>
public class MemberSkill
{
    /// <summary>
    /// Gets or sets the skill identifier.
    /// </summary>
    public string SkillId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
