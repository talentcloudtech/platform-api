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

namespace Platform.Application.Features.Proposal.ApproveRejectProposal;

/// <summary>
/// Handles the request to approve or reject a proposal.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class ApproveRejectProposalHandler(IContext context, ICurrentSession session) : IRequestHandler<ApproveRejectProposalRequest, Result<ProposalResponse>>
{
    /// <summary>
    /// Handles the approve reject proposal request.
    /// </summary>
    /// <param name="request">The approve reject request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated proposal.</returns>
    public async Task<Result<ProposalResponse>> Handle(ApproveRejectProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await context.Proposals.Include(p => p.User).Include(p => p.ProjectTask).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (proposal == null)
            return Result.NotFound("Proposal not found.");

        proposal.Status = request.Status;

        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (request.Status == ProposalStatus.Approved)
            {
                var message = new Message
                {
                    Subject = "Proposal Approved",
                    Body = $"Hi, The proposal for {proposal.ProjectTask.Title} has been approved.",
                    MessageType = MessageType.Notification,
                    SenderId = session.UserId,
                    WorkspaceId = proposal.ProjectTask.WorkspaceId,
                    ProjectId = proposal.ProjectTask.ProjectId,
                    CreatedAt = DateTime.UtcNow,
                    RecipientCount = 1,
                    ReplyAllowed = false,
                };

                message.Recipients.Add(
                    new MessageRecipient
                    {
                        RecipientId = proposal.User.Id,
                        IsRead = false,
                        IsArchived = false,
                        ReceivedAt = DateTime.UtcNow,
                    }
                );

                context.Messages.Add(message);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var response = proposal.Adapt<ProposalResponse>();
            response.Applicant = new CommonUserResponse(proposal.User, proposal.User.MemberProfile);

            return Result.Success(response);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
