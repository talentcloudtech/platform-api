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

namespace Platform.Application.Features.MemberRequests.SearchMemberRequests;

/// <summary>
/// Handles the request to search for member requests with optional filtering and pagination.
/// </summary>
/// <param name="context">The context to access the database.</param>
public class SearchMemberRequestsHandler(IContext context) : IRequestHandler<SearchMemberRequestsRequest, Result<PaginatedList<MemberRequestResponse>>>
{
    /// <summary>
    /// Handles the request to search for member requests based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member requests.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="MemberRequestResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<MemberRequestResponse>>> Handle(SearchMemberRequestsRequest request, CancellationToken cancellationToken)
    {
        var memberRequests = context
            .MemberRequests.Where(x => x.WorkspaceId == request.WorkspaceId)
            .WhereIf(!string.IsNullOrEmpty(request.Title), x => EF.Functions.Like(x.Title, $"%{request.Title}%"))
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status));

        return await memberRequests.ProjectToType<MemberRequestResponse>().OrderBy(x => x.Title).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
