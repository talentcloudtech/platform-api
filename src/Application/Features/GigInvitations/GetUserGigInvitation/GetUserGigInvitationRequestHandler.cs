using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;

namespace Platform.Application.Features.GigInvitations.GetUserGigInvitation;

/// <summary>
/// Handler for getting a paginated list of gig invitations for a user.
/// </summary>
public class GetUserGigInvitationHandler(IContext context) : IRequestHandler<GetUserGigInvitationRequest, Result<PaginatedList<GigInvitationResponse>>>
{
    private readonly IContext _context = context;

    /// <summary>
    /// Handles the request to get a paginated list of gig invitations for a user.
    /// </summary>
    /// <param name="request">The request containing filter and pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing a paginated list of GigInvitationResponse objects.</returns>
    public async Task<Result<PaginatedList<GigInvitationResponse>>> Handle(
        GetUserGigInvitationRequest request,
        CancellationToken cancellationToken)
    {
        var query = _context.GigApplicantInvitations
            .Include(i => i.MemberRequest)
            .Include(i => i.InvitedUser)
            .ThenInclude(r => r!.MemberProfile)
            .Include(i => i.CreatedByUser)
            .ThenInclude(r => r!.MemberProfile)
            .Where(i => i.InvitedUserId == request.UserId);

        // Apply status filter
        if (request.Statuses.Any())
        {
            query = query.Where(i => request.Statuses.Contains(i.Status));
        }

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            // query = query.Where(i => 
            //     EF.Functions.Like(i.MemberRequest?.Title?.ToLower() ?? "", $"%{request.Search.ToLower()}%") ||
            //     EF.Functions.Like(i.MemberRequest.Description.ToLower(), $"%{request.Search.ToLower()}%")
            // );
        }

        // Apply sorting
        query = request.SortOrder.ToLower() == "asc"
            ? query.OrderBy(i => EF.Property<object>(i, request.SortBy))
            : query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        // Execute query and paginate results
        var paginatedInvitations = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        // Map to response objects
        var invitationResponses = paginatedInvitations.Result.Select(i => new GigInvitationResponse
        {
            Id = i.Id,
            Status = i.Status,
            CreatedDate = i.CreatedDate,
            UpdatedDate = i.UpdatedDate,
            MemberRequestId = i.MemberRequestId,
            InvitedUser = new CommonUserResponse(i.InvitedUser!, i.InvitedUser!.MemberProfile),
            InvitedBy = new CommonUserResponse(i.CreatedByUser!, i.CreatedByUser!.MemberProfile),
            CreatedBy = i.CreatedBy,
            UpdatedBy = i.UpdatedBy,
        }).ToList();

        var result = new PaginatedList<GigInvitationResponse>
        {
            CurrentPage = paginatedInvitations.CurrentPage,
            TotalPages = paginatedInvitations.TotalPages,
            TotalItems = paginatedInvitations.TotalItems,
            Result = invitationResponses
        };

        return Result.Success(result);
    }
}