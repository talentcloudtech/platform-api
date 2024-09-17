using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Features.Languages;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberSkills.CreateMemberSkill;

/// <summary>
/// Represents a request to create a new member skill.
/// </summary>
public record CreateMemberSkillRequest : IRequest<Result<MemberSkillResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// Gets or sets the skill identifier.
    /// </summary>
    public string SkillId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the number of months of experience in the skill.
    /// </summary>
    public int MonthsOfExperience { get; set; }
}
