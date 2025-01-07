using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.UpdateTask;

/// <summary>
/// Handles the update of an existing task.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <param name="userManager">The user manager.</param>
/// <returns>The result of the operation.</returns>
public class UpdateTaskHandler(IContext context, ICurrentSession session, UserManager<ApplicationUser> userManager)
    : IRequestHandler<UpdateTaskRequest, Result<TaskResponse>>
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

        var queryTaskList = context.ProjectTasks.Where(x => x.ProjectId == request.ProjectId);
        var previousTaskTotalCost = queryTaskList.Where(x => x.Id != task.Id).Sum(x => x.Cost);
        var totalCost = previousTaskTotalCost + request.Cost ?? 0;

        var isAdmin = session.Role!.Name == UserRole.Admin.Name;

        if (
            !isAdmin
            && !task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId)
            && !task.Project.ProjectMembers.Any(x => x.UserId == session.UserId)
        )
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

        if (request.Type == TaskType.Open)
            task.OpenTaskStatus = OpenTaskStatus.Pending;
        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if ((previousTaskTotalCost / task.Project.Budget).GetValueOrDefault() < 0.8m && (totalCost / task.Project.Budget).GetValueOrDefault() > 0.8m)
            {
                // TODO: Get admin user where userRole is Admin from userManager and get first or default admin user

                var adminUsers = await userManager.GetUsersInRoleAsync(UserRole.Admin.Name).ConfigureAwait(false);
                var adminUser = adminUsers.FirstOrDefault();

                if (adminUser == null)
                    return Result.Error("Admin user not found.");

                var workspaceOwnerIds = task
                    .Project.Workspace.WorkspaceMembers.Where(x => x.Role == WorkspaceRole.WorkspaceOwner)
                    .Select(x => x.UserId)
                    .ToList();
                var projectOwnerIds = task
                    .Project.ProjectMembers.Where(x => x.Role == ProjectRole.ProjectOwner || x.Role == ProjectRole.ProjectManager)
                    .Select(x => x.UserId)
                    .ToList();

                var recipientIds = workspaceOwnerIds.Concat(projectOwnerIds).ToList();

                var message = new Message
                {
                    Subject = $"Project: {task.Project.Title} - Budget Exceeded",
                    Body =
                        $"Hi, This is a notification to inform you that the your project budget has exceeded 80% of the total budget. Please take necessary actions to manage your project budget.",
                    MessageType = MessageType.Notification,
                    SenderId = adminUser.Id,
                    WorkspaceId = task.WorkspaceId,
                    ProjectId = task.ProjectId,
                    CreatedAt = DateTime.UtcNow,
                    RecipientCount = recipientIds.Count,
                    ReplyAllowed = false,
                };

                recipientIds.ForEach(x =>
                {
                    message.Recipients.Add(
                        new MessageRecipient
                        {
                            RecipientId = x,
                            IsRead = false,
                            IsArchived = false,
                            ReceivedAt = DateTime.UtcNow,
                        }
                    );
                });

                await context.Messages.AddAsync(message, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success(task.Adapt<TaskResponse>());
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Error(ex.Message);
        }
    }
}
