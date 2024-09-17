using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Skills.CreateSkill;

/// <summary>
/// Represents a request to create a new skill.
/// </summary>
public record CreateSkillRequest : IRequest<Result<SkillResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the skill.
    /// </summary>
    [JsonIgnore]
    public SkillId Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the skill.
    /// </summary>
    public string Name { get; init; } = null!;
}
