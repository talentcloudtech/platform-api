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

namespace Platform.Application.Features.Tasks.CreateTask;

/// <summary>
/// Handles the creation of a new task.
/// </summary>
public class CreateTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateTaskRequest, Result<TaskResponse>>
{
    /// <summary>
    /// Handles the request to create a new task.
    /// </summary>
    /// <param name="request">The request to create a new task.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<TaskResponse>> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (!project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) && !project.ProjectMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You don't have permission to create tasks in this project.");

        var newTask = request.Adapt<ProjectTask>();
        newTask.Type = TaskType.Internal;
        newTask.CreatedBy = session.UserId;
        newTask.CreatedDate = DateTime.UtcNow;

        context.ProjectTasks.Add(newTask);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newTask.Adapt<TaskResponse>());
    }
}
