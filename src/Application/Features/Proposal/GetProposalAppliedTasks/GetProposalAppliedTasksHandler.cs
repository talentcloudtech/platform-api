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

namespace Platform.Application.Features.Proposal.GetProposalAppliedTasks;

/// <summary>
/// Handles the search for open tasks.
/// </summary>
/// <param name="context"></param>
/// <param name="currentUser"></param>

public class GetProposalAppliedTasksHandler(IContext context, ICurrentSession currentUser)
    : IRequestHandler<GetProposalAppliedTasksRequest, Result<PaginatedList<ProposalResponse>>>
{
    /// <summary>
    /// Handles the search for open tasks.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PaginatedList<ProposalResponse>>> Handle(GetProposalAppliedTasksRequest request, CancellationToken cancellationToken)
    {
        var query = context.Proposals.Include(x => x.User.MemberProfile).Where(x => x.UserId == currentUser.UserId);

        var proposals = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<ProposalResponse>
        {
            CurrentPage = proposals.CurrentPage,
            TotalPages = proposals.TotalPages,
            TotalItems = proposals.TotalItems,
            Result = proposals
                .Result.Select(x =>
                {
                    var proposalResponse = x.Adapt<ProposalResponse>();
                    proposalResponse.Applicant = new CommonUserResponse(x.User, x.User.MemberProfile);
                    return proposalResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
