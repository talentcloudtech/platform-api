using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberRequests.SearchMemberRequests;

/// <summary>
/// Represents a request to search for member requests with optional filtering and pagination.
/// </summary>
/// <param name="Search">The optional search to filter member requests by. If null, no filtering by search is applied.</param>
/// <param name="Statuses">The optional status to filter member requests by. If null, no filtering by status is applied.</param>
/// <param name="SortBy">The optional sort by to filter member requests by. If null, no filtering by sort is applied.</param>
/// <param name="SortOrder">The optional sort order to filter member requests by. If null, no filtering by sort order is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <returns>A paginated list of <see cref="MemberRequestResponse"/> matching the search criteria.</returns>
public record GetPublicGigInvitationRequest(
    string? Search = null,
    MemberRequestStatus[]? Statuses = null,
    string SortBy = nameof(MemberRequest.CreatedDate),
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<MemberRequestResponse>>>;
