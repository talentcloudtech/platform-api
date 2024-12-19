using System;
using System.Collections.Generic;
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

namespace Platform.Application.Features.Proposal.UpdateProposal;

/// <summary>
/// Handles the request to update a proposal.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class UpdateProposalHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateProposalRequest, Result<ProposalResponse>>
{
    /// <summary>
    /// Handles the update proposal request.
    /// </summary>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated proposal.</returns>
    public async Task<Result<ProposalResponse>> Handle(UpdateProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await context.Proposals.Include(p => p.User).Include(p => p.ProjectTask).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (proposal == null)
            return Result.NotFound("Proposal not found.");

        // Update only provided fields
        if (request.Description != null)
            proposal.Description = request.Description;

        if (request.CostEstimation.HasValue)
            proposal.CostEstimation = request.CostEstimation.Value;

        if (request.StartDate.HasValue)
            proposal.StartDate = request.StartDate.Value;

        if (request.EndDate.HasValue)
            proposal.EndDate = request.EndDate.Value;

        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (request.Status.HasValue)
            {
                proposal.Status = request.Status.Value;

                // Send message if proposal is approved
                if (request.Status.Value == ProposalStatus.Approved)
                {
                    var message = new Message
                    {
                        Subject = "Proposal Approved",
                        Body = $"Your proposal for the task {proposal.ProjectTask.Title} has been approved!",
                        MessageType = MessageType.Notification,
                        SenderId = session.UserId,
                        WorkspaceId = proposal.ProjectTask.Project.WorkspaceId,
                        ProjectId = proposal.ProjectTask.ProjectId,
                        CreatedAt = DateTime.UtcNow,
                        RecipientCount = 1,
                        ReplyAllowed = false,
                        Recipients = new List<MessageRecipient>
                        {
                            new MessageRecipient
                            {
                                RecipientId = proposal.User.Id,
                                IsRead = false,
                                IsArchived = false,
                                ReceivedAt = DateTime.UtcNow,
                            },
                        },
                    };

                    context.Messages.Add(message);
                }
            }

            await context.SaveChangesAsync(cancellationToken);

            var response = proposal.Adapt<ProposalResponse>();
            response.Applicant = new CommonUserResponse(proposal.User, proposal.User.MemberProfile);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Error(ex.Message);
        }
    }
}
