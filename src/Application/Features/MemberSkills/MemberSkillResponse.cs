using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberSkills;

/// <summary>
/// Represents the response for a member skill.
/// </summary>
public class MemberSkillResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the language entry.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    /// Gets or sets the skill identifier.
    /// </summary>
    public string SkillId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
