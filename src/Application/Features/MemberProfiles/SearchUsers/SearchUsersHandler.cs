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
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberProfiles.SearchUsers;

/// <summary>
/// Handles the request to search for member profiles with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class SearchUsersHandler(IContext context) : IRequestHandler<SearchUsersRequest, Result<PaginatedList<CommonUserResponse>>>
{
    /// <summary>
    /// Handles the request to search for member profiles based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="MemberProfileResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<CommonUserResponse>>> Handle(SearchUsersRequest request, CancellationToken cancellationToken)
    {
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

        var paginatedResponse = new PaginatedList<CommonUserResponse>
        {
            CurrentPage = memberProfilesResponse.CurrentPage,

            TotalPages = memberProfilesResponse.TotalPages,
            TotalItems = memberProfilesResponse.TotalItems,
            Result = memberProfilesResponse
                .Result.Select(x =>
                {
                    return new CommonUserResponse(x.User!, x);
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
