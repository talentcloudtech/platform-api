using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.UpdateMemberProfile;

/// <summary>
/// Represents a request to update a member profile's details.
/// </summary>
public record UpdateMemberProfileRequest : IRequest<Result<MemberProfileResponse>>
{
    /// <summary>
    /// Gets the unique identifier of the member profile to be updated.
    /// </summary>
    [JsonIgnore]
    public UserId Id { get; init; }

    /// <summary>
    /// Gets or sets the full name of the member.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the user name of the member.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the short biography of the member.
    /// </summary>
    public string? ShortBio { get; set; }

    /// <summary>
    /// Gets or sets the URL of the profile picture.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the subtitle or designation of the member.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the member.
    /// </summary>
    public string? Timezone { get; set; }

    /// <summary>
    /// Gets or sets the address of the member.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the languages known by the member.
    /// </summary>
    public List<string>? Languages { get; set; }

    /// <summary>
    /// Gets or sets the skills of the member.
    /// </summary>
    public List<Skill>? Skills { get; set; }

    /// <summary>
    /// Gets or sets the work experiences of the member.
    /// </summary>
    public List<Experience>? Experiences { get; set; }

    /// <summary>
    /// Gets or sets the educational background of the member.
    /// </summary>
    public List<Education>? Educations { get; set; }

    /// <summary>
    /// Gets or sets the certifications of the member.
    /// </summary>
    public List<Certification>? Certifications { get; set; }

    /// <summary>
    /// Gets or sets the project info of the member.
    /// </summary>
    public List<ProjectInfo>? ProjectInfos { get; set; }

    /// <summary>
    /// Gets or sets the external links associated with the member.
    /// </summary>
    public string[]? ExternalLinks { get; set; }

    /// <summary>
    /// Gets or sets the URL of the resume file.
    /// </summary>
    public string? ResumeFileUrl { get; set; }
}
