using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member request entity in the system.
/// </summary>
public class MemberRequest : AuditableEntity<MemberRequestId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the member request.
    /// </summary>
    public override MemberRequestId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the workspace identifier associated with this member request.
    /// </summary>
    public required WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the title of the request.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the request.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the list of skills required for the request.
    /// </summary>
    public string[] RequiredSkills { get; set; } = [];

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
    public decimal ExpectedBudget { get; set; }

    /// <summary>
    /// Gets or sets the number of minutes per day expected for the task.
    /// </summary>
    public int MinutesPerDay { get; set; }

    /// <summary>
    /// Gets or sets the timezone relevant to the request.
    /// </summary>
    public required string Timezone { get; set; }

    /// <summary>
    /// Gets or sets the current status of the request.
    /// </summary>
    public MemberRequestStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the workspace associated with this member request.
    /// </summary>
    public virtual Workspace? Workspace { get; set; }

    /// <summary>
    /// Gets or sets the list of invitations associated with this member request.
    /// </summary>
    public ICollection<MemberRequestInvitation> Invitations { get; set; } = new List<MemberRequestInvitation>();
}
