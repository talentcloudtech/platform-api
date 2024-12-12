using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.PublishTask;

/// <summary>
/// Handles publishing a task
/// </summary>
public class PublishTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<PublishTaskRequest, Result>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(PublishTaskRequest request, CancellationToken cancellationToken)
    {
        // Verify user is admin
        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("Only administrators can publish tasks.");

        // Get task
        var task = await context.ProjectTasks.FirstOrDefaultAsync(
            t => t.Id == request.TaskId && t.ProjectId == request.ProjectId && t.WorkspaceId == request.WorkspaceId,
            cancellationToken
        );

        if (task == null)
            return Result.NotFound("Task not found.");

        // Check if task is already published
        if (task.IsPublished)
            return Result.Error("Task is already published.");

        // Publish task
        task.IsPublished = true;
        task.UpdatedBy = session.UserId;
        task.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
