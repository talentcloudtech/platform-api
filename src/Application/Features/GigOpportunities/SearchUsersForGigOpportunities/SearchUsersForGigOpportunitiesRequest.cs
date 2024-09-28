using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigOpportunities.SearchUsersForGigOpportunities;

/// <summary>
/// Represents a request to search for member profiles for a gig opportunity.
/// </summary>
/// <param name="Prompt">The optional prompt to filter the search results.</param>
public record SearchUsersForGigOpportunitiesRequest(string? Prompt = null) : IRequest<Result<List<GigOpportunityUserResponse>>>
{
    /// <summary>
    /// The ID of the member request.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId MemberRequestId { get; set; }
}
