using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;

/// <summary>
/// Create task assignee
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class CreateTaskAssigneeHandler(IContext context, ICurrentSession session) : IRequestHandler<CreateTaskAssigneeRequest, Result<TaskAssigneeResponse>>
{
    /// <summary>
    /// Create task assignee
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result.</returns>
    public async Task<Result<TaskAssigneeResponse>> Handle(CreateTaskAssigneeRequest request, CancellationToken cancellationToken)
    {
        var task = await context
            .ProjectTasks.Include(x => x.Project.Workspace.WorkspaceMembers)
            .Include(x => x.Project.ProjectMembers)
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);
        if (task == null)
            return Result.NotFound("Task not found.");

        var hasValidRole =
            task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) || task.Project.ProjectMembers.Any(x => x.UserId == session.UserId);

        if (!hasValidRole)
            return Result.Forbidden("You are not allowed to add assignee to this task.");

        var taskAssignee = request.Adapt<ProjectTaskAssignment>();
        taskAssignee.CreatedBy = session.UserId;
        taskAssignee.CreatedDate = DateTime.UtcNow;
        taskAssignee.AssignedBy = session.UserId;
        taskAssignee.AssignedDate = DateTime.UtcNow;

        await context.ProjectTaskAssignments.AddAsync(taskAssignee, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var response = taskAssignee.Adapt<TaskAssigneeResponse>();
        return Result.Success(response);
    }
}
