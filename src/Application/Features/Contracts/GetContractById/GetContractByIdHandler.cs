using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Contracts.GetContractById;

/// <summary>
/// Handles the request to retrieve a contract by its unique identifier.
/// </summary>
/// <param name="context">The database context used to access contracts.</param>
/// <returns>A <see cref="Result{T}"/> containing a <see cref="ContractResponse"/> if the contract is found, or a not found result if the contract does not exist.</returns>
public class GetContractByIdHandler(IContext context) : IRequestHandler<GetContractByIdRequest, Result<ContractResponse>>
{
    /// <summary>
    /// Handles the retrieval of a contract by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the contract to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="ContractResponse"/> if the contract is found,
    /// or a not found result if the contract does not exist.
    /// </returns>
    public async Task<Result<ContractResponse>> Handle(GetContractByIdRequest request, CancellationToken cancellationToken)
    {
        var contract = await context
            .Contracts.Include(x => x.Workspace.Members)
            .FirstOrDefaultAsync(x => x.WorkspaceId == request.WorkspaceId && x.Id == request.Id, cancellationToken: cancellationToken);

        if (contract is null)
            return Result.NotFound("Contract not found.");

        return contract.Adapt<ContractResponse>();
    }
}
