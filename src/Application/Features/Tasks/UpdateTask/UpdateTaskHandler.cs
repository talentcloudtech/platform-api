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

namespace Platform.Application.Features.Tasks.UpdateTask;

/// <summary>
/// Handles the update of an existing task.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <returns>The result of the operation.</returns>
public class UpdateTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateTaskRequest, Result<TaskResponse>>
{
    /// <summary>
    /// Handles the request to update an existing task.
    /// </summary>
    /// <param name="request">The request to update an existing task.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<TaskResponse>> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
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
            return Result.Forbidden("You don't have permission to update this task.");

        task.Title = request.Title;
        task.Description = request.Description;
        task.StartDate = request.StartDate;
        task.DueDate = request.DueDate;
        task.Estimate = request.Estimate;
        task.Cost = request.Cost;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.IsClosed = request.IsClosed;
        task.Type = request.Type;
        task.UpdatedBy = session.UserId;
        task.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(task.Adapt<TaskResponse>());
    }
}
