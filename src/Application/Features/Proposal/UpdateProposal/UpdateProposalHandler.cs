using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Proposal.UpdateProposal;

/// <summary>
/// Handles the request to update a proposal.
/// </summary>
/// <param name="context">The database context.</param>
public class UpdateProposalHandler(IContext context) : IRequestHandler<UpdateProposalRequest, Result<ProposalResponse>>
{
    /// <summary>
    /// Handles the update proposal request.
    /// </summary>
    /// <param name="request">The update request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The updated proposal.</returns>
    public async Task<Result<ProposalResponse>> Handle(UpdateProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await context.Proposals.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

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

        if (request.Status.HasValue)
            proposal.Status = request.Status.Value;

        await context.SaveChangesAsync(cancellationToken);

        var response = proposal.Adapt<ProposalResponse>();
        response.Applicant = new CommonUserResponse(proposal.User, proposal.User.MemberProfile);

        return Result.Success(response);
    }
}
