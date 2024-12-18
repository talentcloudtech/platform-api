using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using JobBoard.Application.Features.Proposal.GetProposal;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Proposal.GetProposal;

/// <summary>
/// Handles the request to get a proposal by its ID.
/// </summary>
/// <param name="context">The database context.</param>
public class GetProposalHandler(IContext context) : IRequestHandler<GetProposalRequest, Result<ProposalResponse>>
{
    /// <summary>
    /// Handles the request to get a proposal by its ID.
    /// </summary>
    /// <param name="request">The request to get a proposal by its ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The proposal.</returns>

    public async Task<Result<ProposalResponse>> Handle(GetProposalRequest request, CancellationToken cancellationToken)
    {
        var proposal = await context.Proposals.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (proposal == null)
            return Result.NotFound("Proposal not found.");

        var response = proposal.Adapt<ProposalResponse>();
        response.Applicant = new CommonUserResponse(proposal.User, proposal.User.MemberProfile);
        return Result.Success(response);
    }
}
