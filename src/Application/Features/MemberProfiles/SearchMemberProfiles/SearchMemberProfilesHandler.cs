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
public class SearchMemberProfilesHandler(IContext context) : IRequestHandler<SearchMemberProfilesRequest, Result<PaginatedList<MemberProfileResponse>>>
{
    /// <summary>
    /// Handles the request to search for member profiles based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="MemberProfileResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<MemberProfileResponse>>> Handle(SearchMemberProfilesRequest request, CancellationToken cancellationToken)
    {
        var memberProfiles = context
            .MemberProfiles.WhereIf(
                !string.IsNullOrEmpty(request.Name),
                x =>
                    EF.Functions.Like(x.FullName.ToLower().Trim(), $"%{request.Name.ToLower().Trim()}%")
                    || EF.Functions.Like(x.UserName.ToLower().Trim(), $"%{request.Name.ToLower().Trim()}%")
            )
            .AsNoTracking();

        return await memberProfiles.ProjectToType<MemberProfileResponse>().OrderBy(x => x.FullName).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
