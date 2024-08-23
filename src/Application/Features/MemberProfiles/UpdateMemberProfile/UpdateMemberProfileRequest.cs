using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System.Text.Json.Serialization;

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
    public MemberProfileId Id { get; init; }

    /// <summary>
    /// Gets or sets the full name of the member.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the handle name of the member
    /// </summary>
    public required string HandleName { get; set; }

    /// <summary>
    /// Gets or sets the short biography of the member.
    /// </summary>
    public required string ShortBio { get; set; }

    /// <summary>
    /// Gets or sets the URL of the profile picture.
    /// </summary>
    public string? ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the member.
    /// </summary>
    public required string Timezone { get; set; }

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
}
