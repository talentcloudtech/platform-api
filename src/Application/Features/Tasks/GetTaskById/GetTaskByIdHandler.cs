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

namespace Platform.Application.Features.Tasks.GetTaskById;

/// <summary>
/// Handles retrieving a task by its ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <returns>The result of the operation.</returns>
public class GetTaskByIdHandler(IContext context, ICurrentSession session) : IRequestHandler<GetTaskByIdRequest, Result<TaskResponse>>
{
    /// <summary>
    /// Handles the request to get a task by its ID.
    /// </summary>
    /// <param name="request">The request to get a task by its ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<TaskResponse>> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        var task = await context
            .ProjectTasks.Include(x => x.Project.Members)
            .Include(x => x.Project.Workspace.Members)
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && x.ProjectId == request.ProjectId && x.Project.WorkspaceId == request.WorkspaceId,
                cancellationToken
            );

        if (task == null)
            return Result.NotFound("Task not found.");

        if (!task.Project.Members.Any(x => x.UserId == session.UserId) && !task.Project.Workspace.Members.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You don't have permission to view this task.");

        return Result.Success(task.Adapt<TaskResponse>());
    }
}
