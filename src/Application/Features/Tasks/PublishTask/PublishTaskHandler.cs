using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
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

        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var message = new Message
            {
                Subject = "Task Published",
                Body = $"The task {task.Title} has been published!",
                MessageType = MessageType.Notification,
                SenderId = session.UserId,
                WorkspaceId = task.Project.WorkspaceId,
                ProjectId = task.ProjectId,
                CreatedAt = DateTime.UtcNow,
                RecipientCount = 1,
                ReplyAllowed = false,
                Recipients = new List<MessageRecipient>
                {
                    new MessageRecipient
                    {
                        RecipientId = task.CreatedBy,
                        IsRead = false,
                        IsArchived = false,
                        ReceivedAt = DateTime.UtcNow,
                    },
                },
            };

            context.Messages.Add(message);

            // Check if task is already published
            if (task.IsPublished)
                return Result.Error("Task is already published.");

            // Publish task
            task.IsPublished = true;
            task.UpdatedBy = session.UserId;
            task.UpdatedDate = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Error(ex.Message);
        }
    }
}
