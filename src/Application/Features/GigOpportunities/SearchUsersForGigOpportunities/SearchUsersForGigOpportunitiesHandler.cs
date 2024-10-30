using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
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
    : IRequestHandler<SearchUsersForGigOpportunitiesRequest, Result<PaginatedList<GigOpportunityUserResponse>>>
{
    /// <summary>
    /// Handles the request to search for member profiles for a gig opportunity.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating member profiles.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list of <see cref="GigOpportunityUserResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<GigOpportunityUserResponse>>> Handle(
        SearchUsersForGigOpportunitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You are not authorized to access this resource.");

        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .Include(x => x.Invitations)
            .FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);
        if (memberRequest == null)
            return Result.NotFound("Member request not found.");

        // Get users with Admin and Customer roles
        var admins = await userManager.GetUsersInRoleAsync(UserRole.Admin.Name);
        var customers = await userManager.GetUsersInRoleAsync(UserRole.Customer.Name);

        var adminIds = admins.Select(u => u.Id);
        var customerIds = customers.Select(u => u.Id);
        var workspaceMemberIds = memberRequest.Workspace.WorkspaceMembers.Select(x => x.UserId).ToList();
        var invitedUserIds = memberRequest.Invitations.Select(x => x.InvitedUserId).ToList();

        var excludedUserIds = adminIds.Concat(customerIds).Concat(workspaceMemberIds).Concat(invitedUserIds).Distinct().ToList();
        var requiredSkillsIds = memberRequest.RequiredSkills.Select(x => x.SkillId).ToList();

        var query = context.MemberProfiles.Include(x => x.User).Where(x => !excludedUserIds.Contains(x.User!.Id)).AsNoTracking();

        // Filter by required skills
        query = query.Where(x => x.Skills.Any(y => requiredSkillsIds.Contains(y.SkillId)));

        // Filter by profile completeness
        query = query.Where(x => x.IsProfileComplete);

        var memberProfilesResponse = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var random = new Random();
        var paginatedResponse = new PaginatedList<GigOpportunityUserResponse>
        {
            CurrentPage = memberProfilesResponse.CurrentPage,
            TotalPages = memberProfilesResponse.TotalPages,
            TotalItems = memberProfilesResponse.TotalItems,
            Result = memberProfilesResponse
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
                        Score = random.Next(50, 101), // Generate random score between 50 and 100 for now. Later AI will be used to calculate the score.
                    };
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
