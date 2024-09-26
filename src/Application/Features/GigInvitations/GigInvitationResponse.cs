using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using System;

namespace Platform.Application.Features.GigInvitations;

/// <summary>   
/// Represents the response model for a gig invitation.
/// </summary>
public record GigInvitationResponse
{
    /// <summary>
    /// Gets or sets the identifier of the gig invitation.
    /// </summary>
    public GigApplicantInvitationId Id { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the member request.
    /// </summary>
    public MemberRequestId MemberRequestId { get; init; }

    /// <summary>
    /// Gets or sets the status of the gig invitation.
    /// </summary>
    public GigApplicantInvitationStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the user who is invited to the gig.
    /// </summary>
    public CommonUserResponse InvitedUser { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who invited the applicant.
    /// </summary>
    public CommonUserResponse InvitedBy { get; set; } = null!;

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
}
