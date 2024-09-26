using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectInfos.UpdateProjectInfo;

/// <summary>
/// Represents a request to update an existing project for a member profile.
/// </summary>
public record UpdateProjectInfoRequest : IRequest<Result<ProjectInfoResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the member profile.
    /// </summary>
    [JsonIgnore]
    public UserId MemberProfileId { get; init; }

    /// <summary>
    /// Gets or sets the ID of the project to update.
    /// </summary>
    [JsonIgnore]
    public ProjectInfoId ProjectInfoId { get; init; }

    /// <summary>
    /// Gets or sets the title of the project.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the link to the project.
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// Gets or sets a short description of the project.
    /// </summary>
    public string? ShortDescription { get; set; }

    /// <summary>
    /// Gets or sets a list of technologies used in the project.
    /// </summary>
    public List<ShortSkillResponse> Technologies { get; set; } = new();
}
