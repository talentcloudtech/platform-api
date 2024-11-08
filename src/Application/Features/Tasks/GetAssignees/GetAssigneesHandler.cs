using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Tasks.GetAssignees;

/// <summary>
/// Handler for getting task assignees
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class GetAssigneesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetAssigneesRequest, Result<List<AssigneeResponse>>>
{
    /// <summary>
    /// Handles the request to get task assignees
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result.</returns>
    public async Task<Result<List<AssigneeResponse>>> Handle(
        GetAssigneesRequest request,
        CancellationToken cancellationToken)
    {
        var task = await context.ProjectTasks
            .Include(x => x.Project.Workspace.WorkspaceMembers)
            .Include(x => x.Project.ProjectMembers)
            .Include(x => x.Assignments)
                .ThenInclude(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);

        if (task == null)
            return Result.NotFound("Task not found.");

        var hasValidRole = task.Project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) ||
                          task.Project.ProjectMembers.Any(x => x.UserId == session.UserId);

        if (!hasValidRole)
            return Result.Forbidden("You are not allowed to view assignees of this task.");

        // Get all users who assigned tasks
        var assignerIds = task.Assignments.Select(x => x.AssignedBy).Distinct().ToList();
        var assigners = await context.Users
            .Include(x => x.MemberProfile)
            .Where(x => assignerIds.Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, x => x, cancellationToken);

        var assignees = task.Assignments
            .Select(x => new AssigneeResponse
            {
                AssignedUser = new CommonUserResponse(x.User, x.User.MemberProfile),
                AssignedByUser = new CommonUserResponse(assigners[x.AssignedBy], assigners[x.AssignedBy].MemberProfile),
                AssignedDate = x.AssignedDate
            })
            .ToList();

        return Result.Success(assignees);
    }
}