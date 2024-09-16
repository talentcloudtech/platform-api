using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Languages.CreateLanguage;

/// <summary>
/// Represents a request to create a new language entry.
/// </summary>
public record CreateLanguageRequest : IRequest<Result<LanguageResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Gets or sets the proficiency level of the language.
    /// </summary>
    public LanguageLevel ProficiencyLevel { get; init; }
}
