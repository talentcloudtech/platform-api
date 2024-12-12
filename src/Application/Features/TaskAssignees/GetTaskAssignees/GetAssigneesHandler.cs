using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.GetAssignees;

/// <summary>
/// Handler for getting task assignees
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
public class GetTaskAssigneesHandler(IContext context, ICurrentSession session) : IRequestHandler<GetTaskAssigneesRequest, Result<List<CommonUserResponse>>>
{
    /// <summary>
    /// Handles the request to get task assignees
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result.</returns>
    public async Task<Result<List<CommonUserResponse>>> Handle(GetTaskAssigneesRequest request, CancellationToken cancellationToken)
    {
        var currentUserHasAccess = await PermissionHelper.CheckIfCurrentUserHasTaskAccessAsync(context, request.TaskId, session.UserId, cancellationToken);

        var isAdmin = session.Role!.Name == UserRole.Admin.Name;

        if (!currentUserHasAccess && !isAdmin)
            return Result.Forbidden("You do not have access to this project.");

        var taskAssignees = await context
            .ProjectTaskAssignments.Where(x => x.TaskId == request.TaskId)
            .Select(x => new CommonUserResponse(x.User, x.User.MemberProfile))
            .ToListAsync(cancellationToken);

        return Result.Success(taskAssignees);
    }
}
