using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.GigOpportunities;
using Platform.Application.Features.MemberRequests;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigInvitations.GetUserGigInvitation;

/// <summary>
/// Handler for getting a paginated list of gig invitations for a user.
/// </summary>
public class GetUserGigInvitationHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetUserGigInvitationRequest, Result<PaginatedList<MemberRequestResponse>>>
{
    /// <summary>
    /// Handles the request to get a paginated list of member requests for a user.
    /// </summary>
    /// <param name="request">The request containing filter and pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing a paginated list of MemberRequestResponse objects.</returns>
    public async Task<Result<PaginatedList<MemberRequestResponse>>> Handle(GetUserGigInvitationRequest request, CancellationToken cancellationToken)
    {
        // IQueryable<GigApplicantInvitation> query = context
        //     .GigApplicantInvitations.Include(i => i.MemberRequest)
        //     .Include(i => i.InvitedUser!.MemberProfile)
        //     .Include(i => i.CreatedByUser!.MemberProfile)
        //     .Where(i => i.InvitedUserId == session.UserId);

        // // Apply status filter
        // if (request.Statuses.Any())
        //     query = query.Where(i => request.Statuses.Contains(i.Status));

        // // Apply search filter
        // if (!string.IsNullOrWhiteSpace(request.Search))
        //     query = query.Where(i =>
        //         EF.Functions.Like(i.MemberRequest!.Title!.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%")
        //         || EF.Functions.Like(i.MemberRequest!.Description!.ToLower(), $"%{request.Search.ToLower()}%")
        //     );

        // // Apply sorting
        // query =
        //     request.SortOrder.ToLower() == "asc"
        //         ? query.OrderBy(i => EF.Property<object>(i, request.SortBy))
        //         : query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        // // Execute query and paginate results
        // var paginatedInvitations = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        // // Map to response objects
        // var invitationResponses = paginatedInvitations
        //     .Result.Select(i => new GigInvitationResponse
        //     {
        //         Id = i.Id,
        //         Status = i.Status,
        //         CreatedDate = i.CreatedDate,
        //         UpdatedDate = i.UpdatedDate,
        //         MemberRequestId = i.MemberRequestId,
        //         GigOpportunity = i.MemberRequest.Adapt<GigOpportunityResponse>(),
        //         InvitedUser = new CommonUserResponse(i.InvitedUser!, i.InvitedUser!.MemberProfile),
        //         InvitedBy = new CommonUserResponse(i.CreatedByUser!, i.CreatedByUser!.MemberProfile),
        //         CreatedBy = i.CreatedBy,
        //         UpdatedBy = i.UpdatedBy,
        //     })
        //     .ToList();

        // var result = new PaginatedList<GigInvitationResponse>
        // {
        //     CurrentPage = paginatedInvitations.CurrentPage,
        //     TotalPages = paginatedInvitations.TotalPages,
        //     TotalItems = paginatedInvitations.TotalItems,
        //     Result = invitationResponses,
        // };

        // return Result.Success(result);



        // IQueryable<GigApplicantInvitation> query = context
        //     .GigApplicantInvitations.Include(i => i.MemberRequest)
        //     .Include(i => i.InvitedUser!.MemberProfile)
        //     .Include(i => i.CreatedByUser!.MemberProfile)
        //     .Where(i => i.InvitedUserId == session.UserId);

        // var query = context.MemberRequests
        // .Include(i => i.Invitations.Where(x => x.InvitedUserId == session.UserId))
        // .ThenInclude(i => i.InvitedUser!.MemberProfile)
        // .Where(i => i.Status != Domain.Entities.Enums.MemberRequestStatus.Cancelled);

        var query = context
            .MemberRequests.Include(i => i.Invitations.Where(x => x.InvitedUserId == session.UserId))
            .ThenInclude(i => i.InvitedUser!.MemberProfile)
            .Include(i => i.Invitations.Where(x => x.InvitedUserId == session.UserId))
            .ThenInclude(i => i.CreatedByUser!.MemberProfile)
            .Where(mr =>
                mr.Visibility == MemberRequestVisibility.Public
                || (mr.Visibility == MemberRequestVisibility.Invited && mr.Invitations.Any(i => i.InvitedUserId == session.UserId))
            );

        // // Apply status filter
        // if (request.Statuses.Any())
        //     query = query.Where(i => request.Statuses.Contains(i.Status));

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.Search))
            query = query.Where(i =>
                EF.Functions.Like(i.Title!.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%")
                || EF.Functions.Like(i.Description!.ToLower(), $"%{request.Search.ToLower()}%")
            );

        // Apply sorting
        query =
            request.SortOrder.ToLower() == "asc"
                ? query.OrderBy(i => EF.Property<object>(i, request.SortBy))
                : query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        // Execute query and paginate results
        var paginatedInvitations = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        // // Map to response objects
        // //var invitationResponses = paginatedInvitations.Result.Select(i => i.Adapt<MemberRequestResponse>()).ToList();

        // var invitationResponses = paginatedInvitations
        //    .Result.Select(i => new MemberRequestResponse
        //    {
        //        Id = i.Id,
        //        Status = i.Status,

        //     //    CreatedDate = i.CreatedDate,
        //     //    UpdatedDate = i.UpdatedDate,
        //     //    MemberRequestId = i.MemberRequestId,
        //     //    GigOpportunity = i.MemberRequest.Adapt<GigOpportunityResponse>(),
        //     //    InvitedUser = new CommonUserResponse(i.InvitedUser!, i.InvitedUser!.MemberProfile),
        //     //    InvitedBy = new CommonUserResponse(i.CreatedByUser!, i.CreatedByUser!.MemberProfile),
        //     //    CreatedBy = i.CreatedBy,
        //     //    UpdatedBy = i.UpdatedBy,
        //    })
        //    .ToList();

        var paginatedResponse = new PaginatedList<MemberRequestResponse>
        {
            CurrentPage = paginatedInvitations.CurrentPage,
            TotalPages = paginatedInvitations.TotalPages,
            TotalItems = paginatedInvitations.TotalItems,
            Result = paginatedInvitations
                .Result.Select(x =>
                {
                    var memberRequestResponse = x.Adapt<MemberRequestResponse>();
                    if (x.Invitations.Any(i => i.InvitedUserId == session.UserId))
                    {
                        var invitation = x.Invitations.FirstOrDefault(i => i.InvitedUserId == session.UserId);
                        if (invitation != null)
                        {
                            memberRequestResponse.Invitation = new GigInvitationResponse
                            {
                                Id = invitation.Id,
                                Status = invitation.Status,
                                CreatedDate = invitation.CreatedDate,
                                UpdatedDate = invitation.UpdatedDate,
                                MemberRequestId = invitation.MemberRequestId,
                                InvitedUser = new CommonUserResponse(invitation.InvitedUser!, invitation.InvitedUser!.MemberProfile),
                                InvitedBy = new CommonUserResponse(invitation.CreatedByUser!, invitation.CreatedByUser!.MemberProfile),
                                CreatedBy = invitation.CreatedBy,
                                UpdatedBy = invitation.UpdatedBy,
                            };
                        }
                    }
                    return memberRequestResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
