using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.GigOpportunities;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.GigInvitations.GetGigInvitationById;

/// <summary>
/// Handler for getting a gig invitation by its unique identifier.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="session">The current session.</param>
public class GetGigInvitationByIdHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetGigInvitationByIdRequest, Result<GigInvitationResponse>>
{
    /// <summary>
    /// Handles the request to get a gig invitation by its unique identifier.
    /// </summary>
    /// <param name="request">The request containing the gig invitation identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the GigInvitationResponse.</returns>
    public async Task<Result<GigInvitationResponse>> Handle(GetGigInvitationByIdRequest request, CancellationToken cancellationToken)
    {
        var gigInvitation = await context
            .GigApplicantInvitations.Include(i => i.MemberRequest)
            .Include(i => i.InvitedUser!.MemberProfile)
            .Include(i => i.CreatedByUser!.MemberProfile)
            .FirstOrDefaultAsync(i => i.Id == request.Id && i.InvitedUserId == session.UserId, cancellationToken);

        if (gigInvitation == null)
            return Result.NotFound("Gig invitation not found.");

        var response = gigInvitation.Adapt<GigInvitationResponse>();
        response.GigOpportunity = gigInvitation.MemberRequest.Adapt<GigOpportunityResponse>();
        response.InvitedUser = new CommonUserResponse(gigInvitation.InvitedUser!, gigInvitation.InvitedUser!.MemberProfile);
        response.InvitedBy = new CommonUserResponse(gigInvitation.CreatedByUser!, gigInvitation.CreatedByUser!.MemberProfile);

        return response;
    }
}
