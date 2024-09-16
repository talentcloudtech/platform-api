using System.Collections.Generic;
using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.MemberProfiles.CreateMemberProfile;

/// <summary>
/// Represents a request to create a member profile.
/// </summary>
public record CreateMemberProfileRequest : IRequest<Result<MemberProfileResponse>>
{
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
    /// Gets or sets the external links associated with the member.
    /// </summary>
    public string[]? ExternalLinks { get; set; }
}
