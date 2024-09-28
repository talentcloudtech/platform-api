using System;
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

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Update task assignee
/// </summary>
/// <param name="context"></param>
/// <param name="session"></param>
public class UpdateTaskAssigneeHandler(IContext context, ICurrentSession session) : IRequestHandler<UpdateTaskAssigneeRequest, Result<TaskAssigneeResponse>>
{
    /// <summary>
    /// Update task assignee
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<TaskAssigneeResponse>> Handle(UpdateTaskAssigneeRequest request, CancellationToken cancellationToken)
    {
        var taskAssignee = await context.ProjectTaskAssignments.FirstOrDefaultAsync(x => x.Id == request.AssigneeId, cancellationToken);
        if (taskAssignee == null)
            return Result.NotFound("Task assignee not found.");

        var task = await context
            .ProjectTasks.Include(x => x.Project.Workspace.WorkspaceMembers)
            .Include(x => x.Project.ProjectMembers)
            .Include(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);

        if (task == null)
            return Result.NotFound("Task not found.");

        var hasValidRole =
            task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) || task.Project.ProjectMembers.Any(x => x.UserId == session.UserId);

        if (!hasValidRole)
            return Result.Forbidden("You are not allowed to update this task assignee.");

        taskAssignee.TimeSpent = request.TimeSpent;
        taskAssignee.UpdatedBy = session.UserId;
        taskAssignee.UpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        var response = taskAssignee.Adapt<TaskAssigneeResponse>();
        response.User = new CommonUserResponse(taskAssignee.User, taskAssignee.User.MemberProfile);
        return Result.Success(response);
    }
}
