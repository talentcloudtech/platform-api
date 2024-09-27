using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.GigOpportunities.SearchGigOpportunities;

/// <summary>
/// Request to search for gig opportunities
/// </summary>
/// <param name="MemberRequestIds">The member request ids</param>
/// <param name="WorkspaceIds">The workspace ids</param>
/// <param name="Search">The search query</param>
/// <param name="SortBy">The field to sort by. Default is created date.</param>
/// <param name="SortOrder">The order to sort by. Default is desc.</param>
/// <param name="Skills">The skills</param>
/// <param name="CurrentPage">The current page. Default is 1.</param>
/// <param name="PageSize">The page size. Default is 10.</param>
public record SearchGigOpportunitiesRequest(
    MemberRequestId[]? MemberRequestIds,
    WorkspaceId[]? WorkspaceIds,
    string? Search = "",
    string SortBy = nameof(MemberRequest.CreatedDate),
    string SortOrder = "desc",
    string[]? Skills = null,
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<GigOpportunityResponse>>>;
