using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Enums;

namespace Platform.Application.Features.Workspaces.CreateWorkspace;

/// <summary>
/// Handler for creating a new workspace.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class CreateWorkspaceHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateWorkspaceRequest, Result<WorkspaceResponse>>
{
    /// <summary>
    /// Handles the request to create a new workspace.
    /// </summary>
    /// <param name="request">The request to create a new workspace.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<WorkspaceResponse>> Handle(CreateWorkspaceRequest request, CancellationToken cancellationToken)
    {
        var currentTimeStamp = DateTime.UtcNow;

        var newWorkspace = new Workspace
        {
            Name = request.Name,
            ContactEmail = request.ContactEmail,
            Urls = request.Urls,
            Address = request.Address,
            Logo = request.Logo,
            CreatedBy = session.UserId,
            CreatedDate = currentTimeStamp,
            Members = new List<WorkspaceMember>(),
        };
        newWorkspace.Members.Add(
            new WorkspaceMember
            {
                WorkspaceId = newWorkspace.Id,
                UserId = session.UserId,
                Role = WorkspaceRole.WorkspaceOwner,
                CreatedBy = session.UserId,
                CreatedDate = currentTimeStamp,
            }
        );

        context.Workspaces.Add(newWorkspace);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newWorkspace.Adapt<WorkspaceResponse>());
    }
}
