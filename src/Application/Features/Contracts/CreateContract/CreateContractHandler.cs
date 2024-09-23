using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts.CreateContract;

/// <summary>
/// Handles the creation of a contract.
/// </summary>
public class CreateContractHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateContractRequest, Result<ContractResponse>>
{
    /// <summary>
    /// Handles the creation of a contract.
    /// </summary>
    /// <param name="request">The create contract request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the contract creation.</returns>
    public async Task<Result<ContractResponse>> Handle(CreateContractRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);
        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        if (!workspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You are not a owner of this workspace. You can't create a contract.");

        if (!workspace.Members.Any(x => x.UserId == request.UserId))
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorCode = "UserNotInWorkspace",
                    ErrorMessage = "User is not in workspace.",
                }
            );

        if (await context.Contracts.AnyAsync(x => x.UserId == request.UserId, cancellationToken))
        {
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorCode = "UserAlreadyHasContract",
                    ErrorMessage = "User already has a contract.",
                }
            );
        }

        var currentTimestamp = DateTime.UtcNow;

        var newContract = request.Adapt<Contract>();
        newContract.CreatedDate = currentTimestamp;
        newContract.CreatedBy = session.UserId;

        context.Contracts.Add(newContract);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newContract.Adapt<ContractResponse>());
    }
}
