using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Dto;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.GigOpportunities.CreateGigInvitation;

/// <summary>
/// Handles the request to create new gig invitations.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session information.</param>
/// <param name="userManager">The user manager.</param>
/// <param name="emailSender">The email sender.</param>
/// <returns>The result of the operation.</returns>
public class CreateGigInvitationHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    : IRequestHandler<CreateGigInvitationRequest, Result>
{
    /// <summary>
    /// Handles the request to create new gig invitations.
    /// </summary>
    /// <param name="request">The request to create new gig invitations.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(CreateGigInvitationRequest request, CancellationToken cancellationToken)
    {
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You are not authorized to access this resource.");

        // get the member request
        var memberRequest = await context
            .MemberRequests.Include(x => x.Invitations)
            .FirstOrDefaultAsync(x => x.Id == request.MemberRequestId, cancellationToken);

        if (memberRequest == null)
            return Result.NotFound("This gig opportunity does not exist.");

        if (memberRequest.Status != MemberRequestStatus.Published)
            return Result.Forbidden("This gig opportunity is not published.");

        var invitedUsers = await context.Users.Where(x => request.InvitedUserIds.Contains(x.Id)).ToListAsync(cancellationToken);
        if (invitedUsers.Count != request.InvitedUserIds.Count)
            return Result.NotFound("Some of the invited users do not exist.");

        // Get users with Admin and Customer roles
        var adminIds = await userManager.GetUsersInRoleAsync(UserRole.Admin.Name);
        var customerIds = await userManager.GetUsersInRoleAsync(UserRole.Customer.Name);
        var workspaceMemberIds = memberRequest.Workspace.WorkspaceMembers.Select(x => x.UserId).ToList();

        // Combine admin and customer IDs
        var excludedUserIds = adminIds.Select(u => u.Id).Concat(customerIds.Select(u => u.Id)).Concat(workspaceMemberIds).Distinct().ToList();

        // filter already invited users
        var alreadyInvitedUserIds = memberRequest.Invitations.Select(x => x.InvitedUserId).ToList();
        var usersToInvite = request.InvitedUserIds.Except(alreadyInvitedUserIds).Except(excludedUserIds).ToList();

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

        var gigInvitationSendEmails = gigInvitations
            .Select(x => new GigInvitationSendEmailDto(
                x.Id,
                invitedUsers.FirstOrDefault(y => y.Id == x.InvitedUserId)!.Email ?? "",
                invitedUsers.FirstOrDefault(y => y.Id == x.InvitedUserId)!.UserName ?? ""
            ))
            .ToList();
        await emailSender.SendGigInvitationAsync(memberRequest.Title, gigInvitationSendEmails);

        return Result.Success();
    }
}
