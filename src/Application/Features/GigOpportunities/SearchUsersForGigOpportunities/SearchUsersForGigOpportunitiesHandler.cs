using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.SearchUsersForGigOpportunities;

/// <summary>
/// Handles the request to search for member profiles with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <param name="userManager">The user manager.</param>
/// <returns>The result of the operation.</returns>
public class SearchUsersForGigOpportunitiesHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager)
    : IRequestHandler<SearchUsersForGigOpportunitiesRequest, Result<List<GigOpportunityUserResponse>>> // Updated to use GigOpportunityUserResponse
{
    /// <summary>
    /// Handles the request to search for member profiles for a gig opportunity.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of <see cref="GigOpportunityUserResponse"/> matching the search criteria.</returns>
    public async Task<Result<List<GigOpportunityUserResponse>>> Handle(SearchUsersForGigOpportunitiesRequest request, CancellationToken cancellationToken)
    {
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You are not authorized to access this resource.");

        var memberRequest = await context.MemberRequests.FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);
        // Get users with Admin and Customer roles
        var adminIds = await userManager.GetUsersInRoleAsync(UserRole.Admin.Name);
        var customerIds = await userManager.GetUsersInRoleAsync(UserRole.Customer.Name);

        // Combine admin and customer IDs
        var excludedUserIds = adminIds.Select(u => u.Id).Concat(customerIds.Select(u => u.Id)).Distinct().ToList();

        var query = context.MemberProfiles.Include(x => x.User).Where(x => !excludedUserIds.Contains(x.User!.Id));

        var lowercaseSearch = request.Prompt?.ToLower().Trim();
        query = query
            .WhereIf(
                !string.IsNullOrEmpty(lowercaseSearch),
                x =>
                    EF.Functions.Like(x.User!.UserName!.ToLower().Trim(), $"%{lowercaseSearch}%")
                    || EF.Functions.Like(x.User!.MemberProfile!.FullName!.ToLower().Trim(), $"%{lowercaseSearch}%")
                    || EF.Functions.Like(x.User!.Email!.ToLower().Trim(), $"%{lowercaseSearch}%")
            )
            .AsNoTracking();

        var memberProfilesResponse = await query.ToPaginatedListAsync(1, 20);

        var random = new Random();
        var response = new List<GigOpportunityUserResponse>();
        if (memberProfilesResponse.Result.Any())
        {
            response = memberProfilesResponse
                .Result.Select(x =>
                {
                    var commonUserResponse = new CommonUserResponse(x.User!, x);
                    return new GigOpportunityUserResponse
                    {
                        UserId = commonUserResponse.UserId,
                        UserName = commonUserResponse.UserName,
                        FirstName = commonUserResponse.FirstName,
                        LastName = commonUserResponse.LastName,
                        ProfilePictureUrl = commonUserResponse.ProfilePictureUrl,
                        Score = random.Next(50, 101), // Generate random score between 50 and 100
                    };
                })
                .OrderByDescending(x => x.Score) // Order by score in descending order
                .ToList();
        }

        return Result.Success(response);
    }
}
