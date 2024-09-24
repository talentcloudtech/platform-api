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
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Contracts.UpdateContract;

/// <summary>
/// Handles the update contract request.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the update operation.</returns>
public class UpdateContractHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateContractRequest, Result<ContractResponse>>
{
    /// <summary>
    /// Handles the update contract request.
    /// </summary>
    /// <param name="request">The update contract request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<ContractResponse>> Handle(UpdateContractRequest request, CancellationToken cancellationToken)
    {
        var originalContract = await context
            .Contracts.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalContract == null)
            return Result.NotFound("Contract not found.");

        if (!originalContract.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You don't have permission to update this contract.");

        if (!originalContract.Workspace.WorkspaceMembers.Any(x => x.UserId == request.UserId))
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = nameof(request.UserId),
                    ErrorCode = "UserNotInWorkspace",
                    ErrorMessage = "User is not in workspace.",
                }
            );

        originalContract.UserId = request.UserId;
        originalContract.BillingType = request.BillingType;
        originalContract.AvailablePerDay = request.AvailablePerDay;
        originalContract.BillingRate = request.BillingRate;
        originalContract.StartDate = request.StartDate;
        originalContract.EndDate = request.EndDate;
        originalContract.Status = request.Status;
        originalContract.UpdatedBy = session.UserId;
        originalContract.UpdatedDate = DateTime.UtcNow;

        context.Contracts.Update(originalContract);
        await context.SaveChangesAsync(cancellationToken);

        return originalContract.Adapt<ContractResponse>();
    }
}
