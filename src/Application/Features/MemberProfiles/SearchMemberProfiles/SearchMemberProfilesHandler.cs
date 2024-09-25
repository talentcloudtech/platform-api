using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;

namespace Platform.Application.Features.MemberProfiles.SearchMemberProfiles;

/// <summary>
/// Handles the request to search for member profiles with optional filtering and pagination.
/// </summary>
public class SearchMemberProfilesHandler(IContext context) : IRequestHandler<SearchMemberProfilesRequest, Result<PaginatedList<SearchMemberProfileResponse>>>
{
    /// <summary>
    /// Handles the request to search for member profiles based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="MemberProfileResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<SearchMemberProfileResponse>>> Handle(SearchMemberProfilesRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            return Result.Success(new PaginatedList<SearchMemberProfileResponse>());
        }

        var memberProfilesQuery = context
            .MemberProfiles.Include(x => x.User)
            .WhereIf(
                !string.IsNullOrEmpty(request.Name),
                x =>
                    !string.IsNullOrEmpty(x.UserName) && x.UserName.ToLower().Trim() == request.Name.ToLower().Trim()
                    || !string.IsNullOrEmpty(x.User!.Email) && x.User.Email.ToLower().Trim() == request.Name.ToLower().Trim()
            )
            .AsNoTracking();

        var memberProfilesResponse = await memberProfilesQuery.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<SearchMemberProfileResponse>
        {
            CurrentPage = memberProfilesResponse.CurrentPage,

            TotalPages = memberProfilesResponse.TotalPages,
            TotalItems = memberProfilesResponse.TotalItems,
            Result = memberProfilesResponse
                .Result.Select(x =>
                {
                    var nameParts = x.FullName?.Split(' ');
                    var memberResponse = x.Adapt<SearchMemberProfileResponse>();
                    memberResponse.Email = x.User!.Email;
                    return memberResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
