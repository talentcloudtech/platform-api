using System;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers;

/// <summary>
/// Represents a project member response in the system.
/// </summary>
public class ProjectMemberResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the project member.
    /// </summary>
    public ProjectMemberId Id { get; init; }

    /// <summary>
    /// Gets or sets the project identifier associated with this project member.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the date when the project member was created.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this project member.
    /// </summary>
    public UserId CreatedBy { get; init; }

    /// <summary>
    /// Gets or sets the date when the project member was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this project member.
    /// </summary>
    public UserId? UpdatedBy { get; init; }

    /// <summary>
    /// Gets or sets the user associated with this project member.
    /// </summary>
    public CommonUserResponse? User { get; set; }
}
