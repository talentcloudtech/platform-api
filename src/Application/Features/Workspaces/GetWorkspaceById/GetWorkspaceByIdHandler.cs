using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Workspaces.GetWorkspaceById;

/// <summary>
/// Handles the request to retrieve a workspace by its unique identifier.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>
/// A <see cref="Result{T}"/> containing a <see cref="WorkspaceResponse"/> if the workspace is found,
/// or a not found result if the workspace does not exist.
/// </returns>
public class GetWorkspaceByIdHandler(IContext context) : IRequestHandler<GetWorkspaceByIdRequest, Result<WorkspaceResponse>>
{
    /// <summary>
    /// Handles the retrieval of a workspace by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the workspace to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="WorkspaceResponse"/> if the workspace is found,
    /// or a not found result if the workspace does not exist.
    /// </returns>

    public async Task<Result<WorkspaceResponse>> Handle(GetWorkspaceByIdRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken: cancellationToken);

        if (workspace is null)
            return new WorkspaceResponse();

        return workspace.Adapt<WorkspaceResponse>();
    }
}
