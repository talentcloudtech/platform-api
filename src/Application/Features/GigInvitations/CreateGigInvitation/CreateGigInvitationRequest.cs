using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Platform.Application.Features.GigInvitations.CreateGigInvitation;

/// <summary>
/// Represents the request model for creating a gig invitation.
/// </summary>
public record CreateGigInvitationRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the current user identifier associated with this gig invitation.
    /// </summary>
    [JsonIgnore]
    public UserId CurrentUserId { get; set; }

    /// <summary>
    /// Gets or sets the member request identifier associated with this gig invitation.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }

    /// <summary>
    /// Gets or sets the list of user identifiers associated with this gig invitation.
    /// </summary>
    public List<UserId> InvitedUserIds { get; set; } = new();
}
