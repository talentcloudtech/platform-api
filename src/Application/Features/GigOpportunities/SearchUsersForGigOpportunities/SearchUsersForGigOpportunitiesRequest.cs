using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigOpportunities.SearchUsersForGigOpportunities;

/// <summary>
/// Represents a request to search for member profiles for a gig opportunity.
/// </summary>
/// <param name="Prompt">The optional prompt to filter the search results.</param>
/// <param name="CurrentPage">The current page. Default is 1.</param>
/// <param name="PageSize">The page size. Default is 10.</param>
public record SearchUsersForGigOpportunitiesRequest(string? Prompt = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<GigOpportunityUserResponse>>>
{
    /// <summary>
    /// The ID of the member request.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }
}
