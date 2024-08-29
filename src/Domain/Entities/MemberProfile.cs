using System.Collections.Generic;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// /Represents a member profile entity.
/// </summary>
public class MemberProfile : Entity<UserId>
{
    /// <summary>
    /// Gets or sets the unique user identifier of the member profile.
    /// </summary>
    public override UserId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the full name of the member.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// Gets or sets the short biography of the member.
    /// </summary>
    public string? ShortBio { get; set; }

    /// <summary>
    /// Gets or sets the URL of the profile picture.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the member.
    /// </summary>
    public string? Timezone { get; set; }

    /// <summary>
    /// Gets or sets the skills of the member.
    /// </summary>
    public List<Skill>? Skills { get; set; }

    /// <summary>
    /// Gets or sets the skills of the member.
    /// </summary>
    public List<IndustryExperience>? IndustryExperiences { get; set; }

    /// <summary>
    /// Gets or sets the external links associated with the member.
    /// </summary>
    public string[]? ExternalLinks { get; set; }

    /// <summary>
    /// Gets or sets the URL of the resume file.
    /// </summary>
    public string? ResumeFileUrl { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the member profile.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
