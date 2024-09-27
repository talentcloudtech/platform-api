using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigInvitations.CreateGigInvitation;

/// <summary>
/// Handles the request to create new gig invitations.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <returns>The result of the operation.</returns>
public class CreateGigInvitationRequestHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateGigInvitationRequest, Result>
{
    /// <summary>
    /// Handles the request to create new gig invitations.
    /// </summary>
    /// <param name="request">The request to create new gig invitations.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(CreateGigInvitationRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Invitations)
            .FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);

        if (memberRequest == null)
        {
            return Result.NotFound("This opportunity does not exist.");
        }

        // filter already invited users
        var alreadyInvitedUserIds = memberRequest.Invitations.Select(x => x.InvitedUserId).ToList();
        var usersToInvite = request.InvitedUserIds.Except(alreadyInvitedUserIds).ToList();

        var currentTime = DateTime.UtcNow;
        // create gig invitations
        var gigInvitations = usersToInvite
            .Select(userId => new GigApplicantInvitation
            {
                InvitedUserId = userId,
                MemberRequestId = request.MemberRequestId,
                Status = GigApplicantInvitationStatus.Pending,
                CreatedBy = session.UserId,
                CreatedDate = currentTime,
            })
            .ToList();

        // add gig invitations to the member request
        context.GigApplicantInvitations.AddRange(gigInvitations);

        // save changes
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
