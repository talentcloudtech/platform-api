using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;

namespace Platform.Application.Features.Contracts.SearchContracts;

/// <summary>
/// Handles the request to search for contracts with optional filtering and pagination.
/// </summary>
/// <param name="context">The context to access the database.</param>
public class SearchContractsHandler(IContext context) : IRequestHandler<SearchContractsRequest, Result<PaginatedList<ContractResponse>>>
{
    /// <summary>
    /// Handles the request to search for contracts based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating contracts.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of <see cref="ContractResponse"/> matching the search criteria.</returns>
    public async Task<Result<PaginatedList<ContractResponse>>> Handle(SearchContractsRequest request, CancellationToken cancellationToken)
    {
        var contracts = context
            .Contracts.Where(x => x.WorkspaceId == request.WorkspaceId)
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status));

        return await contracts
            .ProjectToType<ContractResponse>()
            .OrderByDescending(x => x.CreatedDate)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
