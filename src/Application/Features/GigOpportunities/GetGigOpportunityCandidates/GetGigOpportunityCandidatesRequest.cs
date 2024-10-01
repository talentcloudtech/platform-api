using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Application.Features.GigInvitations;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.GetGigOpportunityCandidates;

/// <summary>
/// Request to get the candidates for a gig opportunity.
/// </summary>
/// <param name="Statuses">The statuses of the gig applicant invitations.</param>
/// <param name="Search">The search query.</param>
/// <param name="CurrentPage">The current page number.</param>
/// <param name="PageSize">The number of items per page.</param>
public record GetGigOpportunityCandidatesRequest(GigApplicantInvitationStatus[]? Statuses = null, string? Search = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<GigInvitationResponse>>>
{
    /// <summary>
    /// The ID of the member request.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }
}
