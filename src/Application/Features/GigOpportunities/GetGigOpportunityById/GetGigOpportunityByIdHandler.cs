using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.GigInvitations;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.GigOpportunities.GetGigOpportunityById;

/// <summary>
/// Handler for getting a gig opportunity by id
/// </summary>
/// <param name="context">The database context</param>
/// <param name="session">The current session</param>
public class GetGigOpportunityByIdHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetGigOpportunityByIdRequest, Result<GigOpportunityResponse>>
{
    /// <summary>
    /// Handles the request to get a gig opportunity by id
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The gig opportunity</returns>
    public async Task<Result<GigOpportunityResponse>> Handle(GetGigOpportunityByIdRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace)
            .Include(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.memberRequestId, cancellationToken);

        if (memberRequest == null)
            return Result.NotFound("Member request not found");

        var gigInvitation = await context
            .GigApplicantInvitations.Include(i => i.MemberRequest)
            .Include(i => i.InvitedUser!.MemberProfile)
            .Include(i => i.CreatedByUser!.MemberProfile)
            .FirstOrDefaultAsync(i => i.MemberRequestId == request.memberRequestId && i.InvitedUserId == session.UserId, cancellationToken);

        var response = memberRequest.Adapt<GigOpportunityResponse>();
        response.Invitation = gigInvitation?.Adapt<GigInvitationResponse>();
        response.Workspace = memberRequest.Workspace.Adapt<WorkspaceResponse>();
        response.CreatedByUser = new CommonUserResponse(memberRequest.User, memberRequest.User.MemberProfile);
        return response;
    }
}
