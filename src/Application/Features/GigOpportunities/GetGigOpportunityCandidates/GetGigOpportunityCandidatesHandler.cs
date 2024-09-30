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
using Platform.Application.Features.GigInvitations;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigOpportunities.GetGigOpportunityCandidates;

/// <summary>
/// Handles the request to get the candidates for a gig opportunity.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class GetGigOpportunityCandidatesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetGigOpportunityCandidatesRequest, Result<PaginatedList<GigInvitationResponse>>>
{
    /// <summary>
    /// Handles the request to get the candidates for a gig opportunity.
    /// </summary>
    /// <param name="request">The request to get the candidates for a gig opportunity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<PaginatedList<GigInvitationResponse>>> Handle(GetGigOpportunityCandidatesRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(mr => mr.Id == request.MemberRequestId, cancellationToken);

        if (
            session.Role!.Name != UserRole.Admin.Name
            && !memberRequest!.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
        )
            return Result.Forbidden("You are not authorized to access this resource.");

        IQueryable<GigApplicantInvitation> query = context.GigApplicantInvitations.Where(i => i.MemberRequestId == request.MemberRequestId);

        query = query.Include(i => i.CreatedByUser!.MemberProfile).Include(i => i.InvitedUser!.MemberProfile).Include(i => i.MemberRequest);

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var lowercaseSearch = request.Search.ToLower().Trim();
            query = query.Where(i =>
                EF.Functions.Like(i.InvitedUser!.MemberProfile!.FullName!.ToLower().Trim(), $"%{lowercaseSearch}%")
                || EF.Functions.Like(i.InvitedUser!.Email!.ToLower().Trim(), $"%{lowercaseSearch}%")
                || EF.Functions.Like(i.InvitedUser!.UserName!.ToLower().Trim(), $"%{lowercaseSearch}%")
            );
        }

        //Filter by status
        if (request.Statuses is not null && request.Statuses.Any())
            query = query.Where(i => request.Statuses!.Contains(i.Status));

        var gigInvitations = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<GigInvitationResponse>
        {
            CurrentPage = gigInvitations.CurrentPage,
            TotalPages = gigInvitations.TotalPages,
            TotalItems = gigInvitations.TotalItems,
            Result = gigInvitations
                .Result.Select(x =>
                {
                    var gigInvitationResponse = x.Adapt<GigInvitationResponse>();
                    gigInvitationResponse.GigOpportunity = x.MemberRequest.Adapt<GigOpportunityResponse>();
                    gigInvitationResponse.InvitedUser = new CommonUserResponse(x.InvitedUser!, x.InvitedUser!.MemberProfile);
                    gigInvitationResponse.InvitedBy = new CommonUserResponse(x.CreatedByUser!, x.CreatedByUser!.MemberProfile);
                    return gigInvitationResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
