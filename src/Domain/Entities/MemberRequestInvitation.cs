using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a member request invitation entity in the system.
/// </summary>
public class MemberRequestInvitation : AuditableEntity<MemberRequestInvitationId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the member request invitation.
    /// </summary>
    public override MemberRequestInvitationId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the member request identifier associated with this invitation.
    /// </summary>
    public MemberRequestId MemberRequestId { get; set; }

    /// <summary>
    /// Gets or sets the user identifier of the invited user.
    /// </summary>
    public UserId InvitedUserId { get; set; }

    /// <summary>
    /// Gets or sets the status of the invitation.
    /// </summary>
    public InvitationStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the member request associated with this invitation.
    /// </summary>
    public MemberRequest MemberRequest { get; set; } = null!;

    /// <summary>
    /// Gets or sets the invited user associated with this invitation.
    /// </summary>
    public ApplicationUser InvitedUser { get; set; } = null!;
}
