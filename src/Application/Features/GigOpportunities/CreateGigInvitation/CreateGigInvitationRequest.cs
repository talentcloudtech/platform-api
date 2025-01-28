using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using Grpc.Core;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.CreateGigInvitation;

/// <summary>
/// Represents the request model for creating a gig invitation.
/// </summary>
public record CreateGigInvitationRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the member request identifier associated with this gig invitation.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }

    /// <summary>
    /// Gets or sets the list of user identifiers associated with this gig invitation.
    /// </summary>
    public List<UserId> InvitedUserIds { get; set; } = new();

    /// <summary>
    /// Gets or sets the status of the gig invitation.
    /// </summary>
    public GigApplicantInvitationStatus? Status { get; set; }
}
