using System.ComponentModel.DataAnnotations.Schema;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a skill that a member request requires.
/// </summary>
public class MemberRequestSkill
{
    /// <summary>
    /// Gets or sets the identifier of the skill.
    /// </summary>
    public required string Identifier { get; set; }

    /// <summary>
    /// Gets or sets the skill that the member request requires.
    /// </summary>
    public required SkillId SkillId { get; set; }

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public required string Name { get; set; }
}
