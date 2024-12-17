using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Projects;
using Platform.Application.Features.Proposal;
using Platform.Application.Features.Proposal.SearchProposals;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.SearchOpenTasks;

/// <summary>
/// Handles the search for open tasks.
/// </summary>
/// <param name="context"></param>
public class SearchProposalsHandler(IContext context) : IRequestHandler<SearchProposalsRequest, Result<PaginatedList<ProposalResponse>>>
{
    /// <summary>
    /// Handles the search for open tasks.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PaginatedList<ProposalResponse>>> Handle(SearchProposalsRequest request, CancellationToken cancellationToken)
    {
        var query = context.Proposals.Include(i => i.ProjectTask).Include(i => i.User.MemberProfile).Where(x => x.ProjectTask.Id == request.TaskId); // Filter for open tasks only

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
                    proposalResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    return proposalResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
