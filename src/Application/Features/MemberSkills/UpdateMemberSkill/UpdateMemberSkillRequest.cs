using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberSkills.UpdateMemberSkill;

/// <summary>
/// Represents a request to update an existing member skill.
/// </summary>
public record UpdateMemberSkillRequest : IRequest<Result<MemberSkillResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// Gets or sets the unique identifier of the language entry.
    /// </summary>
    [JsonIgnore]
    public MemberSkillId MemberSkillId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the skill identifier.
    /// </summary>
    public string SkillId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
