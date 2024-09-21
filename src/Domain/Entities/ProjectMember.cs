using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a project member entity in the system.
/// </summary>
public class ProjectMember : Entity<ProjectMemberId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the project member.
    /// </summary>
    public override ProjectMemberId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the project identifier associated with this project member.
    /// </summary>
    public ProjectId ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier associated with this project member.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the role of the member in the project.
    /// </summary>
    public ProjectRole Role { get; set; }

    /// <summary>
    /// Gets or sets the allocated minimum minutes per day for this project member.
    /// </summary>
    public int AllocatedMinMinutePerDay { get; set; }

    /// <summary>
    /// Gets or sets the date when the project member was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this project member.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the project member was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this project member.
    /// </summary>
    public UserId? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the project associated with this project member.
    /// </summary>
    public Project Project { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with this project member.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
