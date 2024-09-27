using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigInvitations.UpdateGigInvitationStatus;

/// <summary>
/// Represents the request model for updating a gig applicant invitation status.
/// </summary>
public record UpdateGigInvitationStatusRequest : IRequest<Result<GigInvitationResponse>>
{
    /// <summary>
    /// Gets or sets the gig applicant invitation identifier.
    /// </summary>
    [JsonIgnore]
    public GigApplicantInvitationId GigInvitationId { get; init; }

    /// <summary>
    /// Gets or sets the status of the gig applicant invitation.
    /// </summary>
    public GigApplicantInvitationStatus Status { get; init; }
}
