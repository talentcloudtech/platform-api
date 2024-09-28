using System;
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
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.GigInvitations.UpdateGigInvitationStatus;

/// <summary>
/// Handles the request to update a project member.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <returns>The result of the operation.</returns>
public class UpdateGigInvitationStatusHandler(IContext context, ICurrentSession session)
    : IRequestHandler<UpdateGigInvitationStatusRequest, Result<GigInvitationResponse>>
{
    /// <summary>
    /// Handles the request to update a project member.
    /// </summary>
    /// <param name="request">The request to update a project member.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Result<GigInvitationResponse>> Handle(UpdateGigInvitationStatusRequest request, CancellationToken cancellationToken)
    {
        var gigInvitation = await context
            .GigApplicantInvitations.Include(i => i.MemberRequest)
            .Include(i => i.InvitedUser!.MemberProfile)
            .Include(i => i.CreatedByUser!.MemberProfile)
            .FirstOrDefaultAsync(i => i.Id == request.GigInvitationId && i.InvitedUserId == session.UserId, cancellationToken);

        if (gigInvitation == null)
            return Result.NotFound("Gig invitation not found.");

        var statusTransitionResult = GigApplicantInvitationStatusValidator.ValidateStatusTransition(gigInvitation.Status, request.Status, false, false, true);

        if (!statusTransitionResult.IsSuccess)
            return Result.Invalid(new ValidationError(statusTransitionResult.Errors.FirstOrDefault()));

        gigInvitation.Status = request.Status;
        gigInvitation.UpdatedBy = session.UserId;
        gigInvitation.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        var response = gigInvitation.Adapt<GigInvitationResponse>();
        response.GigOpportunity = gigInvitation.MemberRequest.Adapt<GigOpportunityResponse>();
        response.InvitedUser = new CommonUserResponse(gigInvitation.InvitedUser!, gigInvitation.InvitedUser!.MemberProfile);
        response.InvitedBy = new CommonUserResponse(gigInvitation.CreatedByUser!, gigInvitation.CreatedByUser!.MemberProfile);

        return response;
    }
}
