using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;

namespace Platform.Application.Features.MemberProfiles.SearchUsers;

/// <summary>
/// Represents a request to search for member profiles with optional filtering and pagination.
/// </summary>
/// <param name="Search">The optional search term to filter member profiles by. If null, no filtering by search is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchUsersRequest(string? Search = null, int CurrentPage = 1, int PageSize = 10) : IRequest<Result<PaginatedList<CommonUserResponse>>>;
