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
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.GigOpportunities.SearchGigOpportunities;

/// <summary>
/// Handles the request to search for gig opportunities
/// </summary>
/// <param name="context">The database context</param>
/// <param name="session">The current user session</param>
/// <param name="userManager">The user manager</param>
public class SearchGigOpportunitiesHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager)
    : IRequestHandler<SearchGigOpportunitiesRequest, Result<PaginatedList<GigOpportunityResponse>>>
{
    /// <summary>
    /// Handles the request to search for gig opportunities
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The gig opportunities</returns>
    public async Task<Result<PaginatedList<GigOpportunityResponse>>> Handle(SearchGigOpportunitiesRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await context.Users.FirstOrDefaultAsync(x => x.Id == session.UserId, cancellationToken);
        if (currentUser == null)
            return Result.Unauthorized("User not found.");

        var isAdmin = await userManager.IsInRoleAsync(currentUser, "Admin");
        if (!isAdmin)
            return Result.Forbidden("You are not authorized to access this resource.");

        IQueryable<MemberRequest> query = context.MemberRequests.Include(i => i.Workspace).Include(i => i.User.MemberProfile);

        if (request.WorkspaceIds != null && request.WorkspaceIds.Any())
            query = query.Where(i => request.WorkspaceIds.Contains(i.WorkspaceId));

        if (request.MemberRequestIds != null && request.MemberRequestIds.Any())
            query = query.Where(i => request.MemberRequestIds.Contains(i.Id));

        // Apply status filter
        if (request.Skills != null && request.Skills.Any())
            query = query.Where(i => request.Skills.Any(skill => i.RequiredSkills.Contains(skill)));

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(i =>
                EF.Functions.Like(i.Title!.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%")
                || EF.Functions.Like(i.Description!.ToLower(), $"%{request.Search.ToLower()}%")
            );

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var gigOpportunities = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<GigOpportunityResponse>
        {
            CurrentPage = gigOpportunities.CurrentPage,
            TotalPages = gigOpportunities.TotalPages,
            TotalItems = gigOpportunities.TotalItems,
            Result = gigOpportunities
                .Result.Select(x =>
                {
                    var gigOpportunityResponse = x.Adapt<GigOpportunityResponse>();
                    gigOpportunityResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    return gigOpportunityResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
