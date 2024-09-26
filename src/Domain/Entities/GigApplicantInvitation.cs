using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an invitation for a member request in the system.
/// </summary>
public class GigApplicantInvitation : AuditableEntity<GigApplicantInvitationId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the member request invitation.
    /// </summary>
    public override GigApplicantInvitationId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the identifier of the associated member request.
    /// </summary>
    public required MemberRequestId MemberRequestId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the invited user.
    /// </summary>
    public UserId InvitedUserId { get; set; }

    /// <summary>
    /// Gets or sets the status of the invitation.
    /// </summary>
    public GigApplicantInvitationStatus Status { get; set; } = GigApplicantInvitationStatus.Pending;

    /// <summary>
    /// Gets or sets the associated member request.
    /// </summary>
    public MemberRequest? MemberRequest { get; set; }

    /// <summary>
    /// Gets or sets the invited user.
    /// </summary>
    public ApplicationUser? InvitedUser { get; set; }

    /// <summary>
    /// Gets or sets the user who created the invitation.
    /// </summary>
    public ApplicationUser? CreatedByUser { get; set; }
}

