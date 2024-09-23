using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Educations.UpdateEducation;

/// <summary>
/// Represents the request to update an education.
/// </summary>
public record UpdateEducationRequest : IRequest<Result<EducationResponse>>
{
    /// <summary>
    /// The unique identifier of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// The unique identifier of the education.
    /// </summary>
    [JsonIgnore]
    public EducationId EducationId { get; init; } = null!;

    /// <summary>
    /// Gets or sets the name of the educational institute.
    /// </summary>
    public string? InstitutionName { get; set; }

    /// <summary>
    /// Gets or sets the degree with major.
    /// </summary>
    public string? Degree { get; set; }

    /// <summary>
    /// Gets or sets the field of study.
    /// </summary>
    public string? FieldOfStudy { get; set; }

    /// <summary>
    /// Gets or sets the start date of the education.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the education.
    /// </summary>
    public DateTime? EndDate { get; set; }
}
