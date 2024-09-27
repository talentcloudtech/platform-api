using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.DeleteTask;

/// <summary>
/// Handles the deletion of a task.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <returns>The result of the operation.</returns>
public class DeleteTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteTaskRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a task.
    /// </summary>
    /// <param name="request">The request to delete a task.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await context
            .ProjectTasks.Include(x => x.Project.ProjectMembers)
            .Include(x => x.Project.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && x.ProjectId == request.ProjectId && x.Project.WorkspaceId == request.WorkspaceId,
                cancellationToken
            );

        if (task == null)
            return Result.NotFound("Task not found.");

        if (!task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) && !task.Project.ProjectMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You don't have permission to delete this task.");

        context.ProjectTasks.Remove(task);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
