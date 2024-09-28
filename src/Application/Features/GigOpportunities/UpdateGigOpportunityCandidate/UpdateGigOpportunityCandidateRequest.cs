using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Features.GigInvitations;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.UpdateGigOpportunityCandidate;

/// <summary>
/// Request to update the status of a gig opportunity candidate.
/// </summary>
public record UpdateGigOpportunityCandidateRequest : IRequest<Result<GigInvitationResponse>>
{
    /// <summary>
    /// Gets the unique identifier of the member request to be updated.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; init; }

    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the title of the member request.
    /// </summary>
    public GigApplicantInvitationStatus Status { get; init; }
}
