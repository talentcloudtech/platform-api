using Platform.Application.Common.Responses;
using Platform.Domain.Entities;

namespace Platform.Application.Features.GigOpportunities.SearchUsersForGigOpportunities;

/// <summary>
/// Represents a user response for a gig opportunity.
/// </summary>
public class GigOpportunityUserResponse : CommonUserResponse
{
    /// <summary>
    /// The score of the user for the gig opportunity.
    /// </summary>
    public decimal Score { get; set; }
}
