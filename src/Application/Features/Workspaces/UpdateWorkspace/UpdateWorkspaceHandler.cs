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
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.UpdateWorkspace;

/// <summary>
/// Handler for creating a new workspace.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class UpdateWorkspaceHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateWorkspaceRequest, Result<WorkspaceResponse>>
{
    /// <summary>
    /// Handles the request to create a new workspace.
    /// </summary>
    /// <param name="request">The request to create a new workspace.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<WorkspaceResponse>> Handle(UpdateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var currentTimeStamp = DateTime.UtcNow;

        var existingWorkspace = await context.Workspaces.Include(x => x.Members).FirstOrDefaultAsync(predicate: x => x.Id == request.Id, cancellationToken);

        if (existingWorkspace == null)
            return Result.NotFound("Workspace not found.");

        if (!existingWorkspace.Members.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
        {
            return Result.Forbidden("You are not allowed to update this workspace.");
        }

        existingWorkspace.Name = request.Name;
        existingWorkspace.ContactEmail = request.ContactEmail;
        existingWorkspace.Address = request.Address;
        existingWorkspace.Logo = request.Logo;
        existingWorkspace.UpdatedBy = session.UserId;
        existingWorkspace.UpdatedDate = currentTimeStamp;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(existingWorkspace.Adapt<WorkspaceResponse>());
    }
}
