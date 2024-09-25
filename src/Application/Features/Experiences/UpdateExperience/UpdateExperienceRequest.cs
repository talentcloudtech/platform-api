using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Experiences.UpdateExperience;

/// <summary>
/// Represents a request to update an experience.
/// </summary>
public record UpdateExperienceRequest : IRequest<Result<ExperienceResponse>>
{
    /// <summary>
    /// Gets or sets the unique identifier of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// Gets or sets the unique identifier of the experience.
    /// </summary>
    [JsonIgnore]
    public ExperienceId ExperienceId { get; init; }

    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string JobTitle { get; set; } = null!;

    /// <summary>
    /// Gets or sets the industry of the experience.
    /// </summary>
    public string Industry { get; set; } = null!;

    /// <summary>
    /// Gets or sets the start date of the experience.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the experience. Null if currently working.
    /// </summary>
    public DateTime? EndDate { get; set; }
}
