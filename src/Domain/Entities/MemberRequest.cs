using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member request in the system.
/// </summary>
public class MemberRequest : AuditableEntity<MemberRequestId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the member request.
    /// </summary>
    public override MemberRequestId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the workspace identifier associated with this request.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the title of the request.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the request.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the list of skills required for the request.
    /// </summary>
    public required List<MemberRequestSkill> RequiredSkills { get; set; }

    /// <summary>
    /// Gets or sets the reason the request was rejected.
    /// </summary>
    public string? RejectedReason { get; set; }

    /// <summary>
    /// Gets or sets the date the request is scheduled to start.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the deadline or end date for the request.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the expected budget for the request.
    /// </summary>
    public required string ExpectedBudget { get; set; }

    /// <summary>
    /// Gets or sets the number of work hour.
    /// </summary>
    public required string WorkHour { get; set; }

    /// <summary>
    /// Gets or sets the timezone relevant to the request.
    /// </summary>
    public required string Timezone { get; set; }

    /// <summary>
    /// Gets or sets the visibility of the member request.
    /// </summary>
    public MemberRequestVisibility Visibility { get; set; }

    /// <summary>
    /// Gets or sets the type of the member request.
    /// </summary>
    public MemberRequestType Type { get; set; }

    /// <summary>
    /// Gets or sets the current status of the request.
    /// </summary>
    public required MemberRequestStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this request.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;

    /// <summary>
    /// Gets or sets the application associated with this request.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of invitations for this member request.
    /// </summary>
    public List<GigApplicantInvitation> Invitations { get; set; } = [];
}
