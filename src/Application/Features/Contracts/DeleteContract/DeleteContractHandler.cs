using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts.DeleteContract;

/// <summary>
/// Handles the request to delete a contract.
/// </summary>
/// <param name="context">The database context used to access contracts.</param>
/// <param name="session">The current session information.</param>
/// <returns>The result of the delete operation.</returns>
public class DeleteContractHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteContractRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a contract.
    /// </summary>
    /// <param name="request">The request to delete a contract.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteContractRequest request, CancellationToken cancellationToken)
    {
        var contract = await context
            .Contracts.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (contract == null)
            return Result.NotFound("Contract not found.");

        if (!contract.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You don't have permission to delete this contract.");

        context.Contracts.Remove(contract);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
