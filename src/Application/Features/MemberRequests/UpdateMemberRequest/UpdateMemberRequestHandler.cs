using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberRequests.UpdateMemberRequest;

/// <summary>
/// Handles the update member request request.
/// </summary>
public class UpdateMemberRequestHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager)
    : IRequestHandler<UpdateMemberRequestRequest, Result<MemberRequestResponse>>
{
    /// <summary>
    /// Handles the update member request request.
    /// </summary>
    /// <param name="request">The update member request request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<MemberRequestResponse>> Handle(UpdateMemberRequestRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (memberRequest == null)
            return Result.NotFound("Member request not found.");

        var isAdmin = session.Role!.Name == UserRole.Admin.Name;
        if (
            !isAdmin
            && !memberRequest.Workspace.WorkspaceMembers.Any(x =>
                x.UserId == session.UserId && (x.Role == WorkspaceRole.WorkspaceOwner || x.Role == WorkspaceRole.WorkspaceManager)
            )
        )
            return Result.Forbidden("You don't have permission to update this member request.");

        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (var field in request.FieldsToUpdate)
            {
                if (_updateActions.TryGetValue(field, out var action))
                {
                    action(memberRequest, request);
                }
                if (field.Equals("status", StringComparison.OrdinalIgnoreCase) && request.Status.HasValue)
                {
                    var statusTransitionResult = ValidateStatusTransition(memberRequest.Status, request.Status.Value);
                    if (!statusTransitionResult.IsSuccess)
                        return Result.Invalid(
                            new ValidationError
                            {
                                Identifier = "status",
                                ErrorMessage = statusTransitionResult.Errors.FirstOrDefault(),
                                ErrorCode = "InvalidStatusTransition",
                            }
                        );

                    if (request.Status.Value == MemberRequestStatus.Published && memberRequest.Visibility == MemberRequestVisibility.Public)
                    {
                        await CreateGIgInvitationsForAllUsers(memberRequest.Id);
                    }
                    memberRequest.Status = request.Status.Value;
                }
                if (field.Equals("rejectedReason", StringComparison.OrdinalIgnoreCase) && request.RejectedReason != null)
                {
                    memberRequest.RejectedReason = request.RejectedReason;
                }

                if (isAdmin && field.Equals("visibility", StringComparison.OrdinalIgnoreCase) && request.Visibility.HasValue)
                {
                    // Only fetch userIds if visibility is being set to Public
                    if (
                        memberRequest.Visibility == MemberRequestVisibility.Invited
                        && request.Visibility.Value == MemberRequestVisibility.Public
                        && memberRequest.Status == MemberRequestStatus.Published
                    )
                    {
                        await CreateGIgInvitationsForAllUsers(memberRequest.Id);
                    }

                    memberRequest.Visibility = request.Visibility.Value;
                }
            }

            memberRequest.UpdatedBy = session.UserId;
            memberRequest.UpdatedDate = DateTime.UtcNow;

            context.MemberRequests.Update(memberRequest);
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success(memberRequest.Adapt<MemberRequestResponse>());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Error(ex.Message);
        }
    }

    /// <summary>
    /// The update actions for the member request.
    /// </summary>
    private readonly Dictionary<string, Action<MemberRequest, UpdateMemberRequestRequest>> _updateActions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["title"] = (mr, req) =>
            {
                mr.Title = req.Title ?? "";
            },
            ["description"] = (mr, req) => mr.Description = req.Description,
            ["startdate"] = (mr, req) =>
            {
                if (req.StartDate.HasValue)
                    mr.StartDate = req.StartDate.Value;
            },
            ["enddate"] = (mr, req) =>
            {
                if (req.EndDate.HasValue)
                    mr.EndDate = req.EndDate.Value;
            },
            ["expectedbudget"] = (mr, req) =>
            {
                mr.ExpectedBudget = req.ExpectedBudget ?? "";
            },
            ["workhour"] = (mr, req) =>
            {
                mr.WorkHour = req.WorkHour ?? "";
            },
            ["timezone"] = (mr, req) =>
            {
                mr.Timezone = req.Timezone ?? "";
            },
            ["requiredskills"] = (mr, req) =>
            {
                var requiredSkills = req.RequiredSkills!.Distinct().ToList();
                var skills = context.Skills.Where(x => requiredSkills.Contains(x.Id)).ToList();
                mr.RequiredSkills = skills.Select(x => new MemberRequestSkill { SkillId = x.Id, Name = x.Name }).ToList();
            },
        };

    /// <summary>
    /// Validates the status transition.
    /// </summary>
    /// <param name="currentStatus">The current status.</param>
    /// <param name="newStatus">The new status.</param>
    /// <returns>The result of the validation.</returns>
    private Result ValidateStatusTransition(MemberRequestStatus currentStatus, MemberRequestStatus newStatus)
    {
        var validTransitions = GetValidTransitions(currentStatus);
        if (!validTransitions.Contains(newStatus))
        {
            return Result.Error(
                $"Invalid status transition from {currentStatus} to {newStatus}. " + $"Valid transitions are: {string.Join(", ", validTransitions)}"
            );
        }
        return Result.Success();
    }

    /// <summary>
    /// Gets the valid transitions for the member request status.
    /// </summary>
    /// <param name="currentStatus">The current status.</param>
    /// <returns>The valid transitions.</returns>
    private MemberRequestStatus[] GetValidTransitions(MemberRequestStatus currentStatus)
    {
        return currentStatus switch
        {
            MemberRequestStatus.Draft => new[] { MemberRequestStatus.PendingReview, MemberRequestStatus.Published, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.PendingReview => new[]
            {
                MemberRequestStatus.Published,
                MemberRequestStatus.Rejected,
                MemberRequestStatus.Closed,
                MemberRequestStatus.Cancelled,
            },
            MemberRequestStatus.Published => new[] { MemberRequestStatus.InvitationsSent, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.InvitationsSent => new[] { MemberRequestStatus.Shortlisting, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.Shortlisting => new[]
            {
                MemberRequestStatus.AwaitingCustomerSelection,
                MemberRequestStatus.Closed,
                MemberRequestStatus.Cancelled,
            },
            MemberRequestStatus.AwaitingCustomerSelection => new[]
            {
                MemberRequestStatus.InterviewScheduled,
                MemberRequestStatus.Hired,
                MemberRequestStatus.Closed,
                MemberRequestStatus.Cancelled,
            },
            MemberRequestStatus.InterviewScheduled => new[] { MemberRequestStatus.InInterview, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.InInterview => new[] { MemberRequestStatus.OfferExtended, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.OfferExtended => new[] { MemberRequestStatus.Hired, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.Hired => new[] { MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.Rejected => new[] { MemberRequestStatus.PendingReview, MemberRequestStatus.Closed, MemberRequestStatus.Cancelled },
            MemberRequestStatus.Closed => Array.Empty<MemberRequestStatus>(),
            MemberRequestStatus.Cancelled => new[] { MemberRequestStatus.PendingReview },
            _ => Array.Empty<MemberRequestStatus>(),
        };
    }

    /// <summary>
    /// Creates gig invitations for all users.
    /// </summary>
    /// <param name="memberRequestId">The member request id.</param>
    /// <returns>The task.</returns>
    private async Task CreateGIgInvitationsForAllUsers(MemberRequestId memberRequestId)
    {
        var userIds = await userManager.GetUsersInRoleAsync(UserRole.User.Name);
        var currentTime = DateTime.UtcNow;

        var gigInvitations = userIds
            .Select(user => new GigApplicantInvitation
            {
                InvitedUserId = user.Id,
                MemberRequestId = memberRequestId,
                Status = GigApplicantInvitationStatus.Pending,
                CreatedBy = session.UserId,
                CreatedDate = currentTime,
            })
            .ToList();

        context.GigApplicantInvitations.AddRange(gigInvitations);
    }
}
