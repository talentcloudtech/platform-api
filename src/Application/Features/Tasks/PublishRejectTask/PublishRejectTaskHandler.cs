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

namespace Platform.Application.Features.Tasks.PublishRejectTask;

/// <summary>
/// Handles publishing a task
/// </summary>
public class PublishRejectTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<PublishRejectTaskRequest, Result>
{
    /// <inheritdoc/>
    public async Task<Result> Handle(PublishRejectTaskRequest request, CancellationToken cancellationToken)
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

        var message = new Message();
        message.MessageType = MessageType.Notification;
        message.SenderId = session.UserId;
        message.CreatedAt = DateTime.UtcNow;
        message.RecipientCount = 1;
        message.ReplyAllowed = false;
        message.Recipients = new List<MessageRecipient>
        {
            new MessageRecipient
            {
                RecipientId = task.CreatedBy,
                IsRead = false,
                IsArchived = false,
                ReceivedAt = DateTime.UtcNow,
            },
        };

        if (request.Status == OpenTaskStatus.Published)
        {
            message.Subject = "Task Published";
            message.Body = $"The task {task.Title} has been published!";
            task.OpenTaskStatus = OpenTaskStatus.Published;
        }
        else if (request.Status == OpenTaskStatus.Rejected)
        {
            message.Subject = "Task Rejected";
            message.Body = $"The task {task.Title} has been rejected! Reason: {request.Reason}";
            task.OpenTaskStatus = OpenTaskStatus.Rejected;
        }
        task.UpdatedBy = session.UserId;
        task.UpdatedDate = DateTime.UtcNow;

        try
        {
            context.Messages.Add(message);
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
