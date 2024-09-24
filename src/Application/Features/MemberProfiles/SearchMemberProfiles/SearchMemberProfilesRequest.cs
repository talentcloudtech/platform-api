using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;

namespace Platform.Application.Features.MemberProfiles.SearchMemberProfiles;

/// <summary>
/// Represents a request to search for member profiles with optional filtering and pagination.
/// </summary>
/// <param name="Name">The optional name to filter member profiles by. If null, no filtering by name is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchMemberProfilesRequest(string? Name = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<SearchMemberProfileResponse>>>;
