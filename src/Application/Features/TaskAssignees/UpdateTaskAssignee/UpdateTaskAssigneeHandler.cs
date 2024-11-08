using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Update task assignees
/// </summary>
public class UpdateTaskAssigneeHandler(IContext context, ICurrentSession session)
    : IRequestHandler<UpdateTaskAssigneeRequest, Result<List<TaskAssigneeResponse>>>
{
    /// <summary>
    /// Update task assignees
    /// </summary>
    public async Task<Result<List<TaskAssigneeResponse>>> Handle(UpdateTaskAssigneeRequest request, CancellationToken cancellationToken)
    {
        var currentUserHasAccess = await PermissionHelper.CheckIfCurrentUserHasTaskAccessAsync(context, request.TaskId, session.UserId, cancellationToken);

        if (!currentUserHasAccess)
            return Result.Forbidden("You are not allowed to update this task assignee.");

        // Get existing assignments
        var existingAssignments = await context.ProjectTaskAssignments.Where(x => x.TaskId == request.TaskId).ToListAsync(cancellationToken);

        var existingUserIds = existingAssignments.Select(x => x.UserId).ToHashSet();
        var requestedUserIds = request.UserIds.ToHashSet();

        // Find assignments to remove
        var assignmentsToRemove = existingAssignments.Where(x => !requestedUserIds.Contains(x.UserId)).ToList();

        // Find new users to assign
        var usersToAdd = requestedUserIds.Where(userId => !existingUserIds.Contains(userId)).ToList();

        var currentTime = DateTime.UtcNow;

        // Remove assignments
        if (assignmentsToRemove.Any())
        {
            context.ProjectTaskAssignments.RemoveRange(assignmentsToRemove);
        }

        // Add new assignments
        if (usersToAdd.Any())
        {
            var newAssignments = usersToAdd.Select(userId => new ProjectTaskAssignment
            {
                TaskId = request.TaskId,
                UserId = userId,
                TimeSpent = 0,
                AssignedBy = session.UserId,
                AssignedDate = currentTime,
                CreatedBy = session.UserId,
                CreatedDate = currentTime,
                UpdatedBy = session.UserId,
                UpdatedDate = currentTime,
            });

            await context.ProjectTaskAssignments.AddRangeAsync(newAssignments, cancellationToken);
        }

        if (assignmentsToRemove.Any() || usersToAdd.Any())
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        // Get updated assignees
        var updatedAssignees = await context
            .ProjectTaskAssignments.Where(x => x.TaskId == request.TaskId)
            .Select(x => new TaskAssigneeResponse
            {
                UserId = x.UserId,
                TaskId = x.TaskId,
                TimeSpent = x.TimeSpent,
            })
            .ToListAsync(cancellationToken);

        return Result.Success(updatedAssignees);
    }
}
