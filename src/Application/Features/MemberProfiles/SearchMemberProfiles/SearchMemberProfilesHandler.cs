using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
public class SearchMemberProfilesHandler
    : IRequestHandler<SearchMemberProfilesRequest, PaginatedList<MemberProfileResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchMemberProfilesHandler"/> class.
    /// </summary>
    /// <param name="context">The context to access the database.</param>
    public SearchMemberProfilesHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the request to search for member profiles based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="MemberProfileResponse"/> matching the search criteria.</returns>
    public async Task<PaginatedList<MemberProfileResponse>> Handle(
        SearchMemberProfilesRequest request,
        CancellationToken cancellationToken
    )
    {
        var memberProfiles = _context
            .MemberProfiles.WhereIf(
                !string.IsNullOrEmpty(request.Name),
                x => EF.Functions.Like(x.FullName, $"%{request.Name}%")
            )
            .AsNoTracking();

        return await memberProfiles
            .ProjectToType<MemberProfileResponse>()
            .OrderBy(x => x.FullName)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
