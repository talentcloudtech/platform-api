using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.Tasks.CreateTask;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.CreateProposal;

/// <summary>
/// Handles the creation of a new proposal.
/// </summary>
public class CreateProposalHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateProposalRequest, Result<ProposalResponse>>
{
    /// <summary>
    /// Handles the request to create a new task.
    /// </summary>
    /// <param name="request">The request to create a new task.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<ProposalResponse>> Handle(CreateProposalRequest request, CancellationToken cancellationToken)
    {
        var task = await context
            .ProjectTasks.Include(x => x.Project)
            .ThenInclude(p => p.ProjectMembers)
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);

        if (task == null)
            return Result.NotFound("Task not found.");

        if (!task.Project.ProjectMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You don't have permission to create proposals in this project.");

        var proposal = request.Adapt<Domain.Entities.Proposal>();
        proposal.TaskId = task.Id;
        proposal.UserId = session.UserId;
        proposal.CreatedBy = session.UserId;
        proposal.CreatedDate = DateTime.UtcNow;
        proposal.Status = ProposalStatus.Pending;

        context.Proposals.Add(proposal);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(proposal.Adapt<ProposalResponse>());
    }
}
