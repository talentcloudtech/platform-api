using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Application.Features.MemberProfiles.SearchUsers;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberProfiles.SearchMemberBalances;

/// <summary>
/// Handles the request to search for member profiles with optional filtering and pagination.
/// </summary>
/// <param name="session">The current session information.</param>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class SearchMemberBalancesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchMemberBalancesRequest, Result<PaginatedList<SearchMemberBalanceResponse>>>
{
    /// <summary>
    /// Handles the request to search for member profiles based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="SearchMemberBalanceResponse"/> matching the search criteria.</returns>
    ///

    public async Task<Result<PaginatedList<SearchMemberBalanceResponse>>> Handle(SearchMemberBalancesRequest request, CancellationToken cancellationToken)
    {
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You are not authorized to access this resource.");

        var lowercaseSearch = request.Search?.ToLower().Trim();
        IQueryable<MemberProfile> query = context.MemberProfiles.Include(x => x.User);

        query = query
            .WhereIf(
                !string.IsNullOrEmpty(lowercaseSearch),
                x =>
                    EF.Functions.Like(x.User!.UserName!.ToLower().Trim(), $"%{lowercaseSearch}%")
                    || EF.Functions.Like(x.User!.MemberProfile!.FullName!.ToLower().Trim(), $"%{lowercaseSearch}%")
                    || EF.Functions.Like(x.User!.Email!.ToLower().Trim(), $"%{lowercaseSearch}%")
            )
            .AsNoTracking();

        var memberProfilesResponse = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<SearchMemberBalanceResponse>
        {
            CurrentPage = memberProfilesResponse.CurrentPage,

            TotalPages = memberProfilesResponse.TotalPages,
            TotalItems = memberProfilesResponse.TotalItems,
            Result = memberProfilesResponse
                .Result.Select(x =>
                {
                    return new SearchMemberBalanceResponse { User = new CommonUserResponse(x.User!, x), CurrentBalance = x.User!.CurrentBalance ?? 0 };
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
