using System.Collections.Generic;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles;

/// <summary>
/// Represents the response model for a member profile in the application.
/// </summary>
public record MemberProfileResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the member profile.
    /// </summary>
    public UserId Id { get; set; }

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
    public List<Language> Languages { get; set; } = new List<Language>();

    /// <summary>
    /// Gets or sets the skills of the member.
    /// </summary>
    public List<MemberSkill> Skills { get; set; } = new List<MemberSkill>();

    /// <summary>
    /// Gets or sets the work experiences of the member.
    /// </summary>
    public List<Experience> Experiences { get; set; } = new List<Experience>();

    /// <summary>
    /// Gets or sets the educational background of the member.
    /// </summary>
    public List<Education> Educations { get; set; } = new List<Education>();

    /// <summary>
    /// Gets or sets the certifications of the member.
    /// </summary>
    public List<Certification> Certifications { get; set; } = new List<Certification>();

    /// <summary>
    /// Gets or sets the project info of the member.
    /// </summary>
    public List<ProjectInfo> ProjectInfos { get; set; } = new List<ProjectInfo>();

    /// <summary>
    /// Gets or sets the external links associated with the member.
    /// </summary>
    public string[] ExternalLinks { get; set; } = [];

    /// <summary>
    /// Gets or sets the URL of the resume file.
    /// </summary>
    public string? ResumeFileUrl { get; set; }

    /// <summary>
    /// Gets or sets the score of the member profile.
    /// </summary>
    public bool IsProfileComplete { get; set; }

    /// <summary>
    /// Gets or sets the completeness score of the member profile.
    /// </summary>
    public int ProfileCompletenessScore { get; set; }
}
