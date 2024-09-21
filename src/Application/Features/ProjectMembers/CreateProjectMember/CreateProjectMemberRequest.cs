using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers.CreateProjectMember;

/// <summary>
/// Represents a request to create a new project member.
/// </summary>
public record CreateProjectMemberRequest : IRequest<Result<ProjectMemberResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier associated with this project member.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project identifier associated with this project member.
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the user identifier associated with this project member.
    /// </summary>
    public UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the role of the member in the project.
    /// </summary>
    public ProjectRole Role { get; init; }

    /// <summary>
    /// Gets or sets the allocated minimum minutes per day for this project member.
    /// </summary>
    public int AllocatedMinMinutePerDay { get; init; }
}
