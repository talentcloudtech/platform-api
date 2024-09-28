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
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities;

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
        IQueryable<MemberRequest> query = context.MemberRequests.Include(i => i.Workspace).Include(i => i.User.MemberProfile);

        query = query.Where(x => x.WorkspaceId == request.WorkspaceId);

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        if (request.Statuses!.Any())
            query = query.Where(x => request.Statuses!.Contains(x.Status));

        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var memberRequests = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<MemberRequestResponse>
        {
            CurrentPage = memberRequests.CurrentPage,
            TotalPages = memberRequests.TotalPages,
            TotalItems = memberRequests.TotalItems,
            Result = memberRequests
                .Result.Select(x =>
                {
                    var memberRequestResponse = x.Adapt<MemberRequestResponse>();
                    memberRequestResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    memberRequestResponse.Workspace = x.Workspace.Adapt<WorkspaceResponse>();
                    return memberRequestResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
