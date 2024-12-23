using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Projects;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.GetTaskById;

/// <summary>
/// Handles retrieving a task by its ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current user session.</param>
/// <returns>The result of the operation.</returns>
public class GetTaskByIdHandler(IContext context, ICurrentSession session) : IRequestHandler<GetTaskByIdRequest, Result<TaskResponse>>
{
    /// <summary>
    /// Handles the request to get a task by its ID.
    /// </summary>
    /// <param name="request">The request to get a task by its ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<TaskResponse>> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        var task = await context
            .ProjectTasks.Include(x => x.Project.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .Include(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.ProjectId == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (task == null)
            return Result.NotFound("Task not found.");
        var isAdmin = session.Role!.Name == UserRole.Admin.Name;

        if (
            !isAdmin
            && task.Type == TaskType.Internal
            && !task.Project.ProjectMembers.Any(x => x.UserId == session.UserId)
            && !task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId)
        )
            return Result.Forbidden("You don't have permission to view this task.");

        var response = task.Adapt<TaskResponse>();

        response.CreatedByUser = new CommonUserResponse(task.User, task.User.MemberProfile);
        response.Workspace = task.Workspace.Adapt<WorkspaceResponse>();
        response.Project = task.Project.Adapt<ProjectResponse>();

        return Result.Success(response);
    }
}
