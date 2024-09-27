using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Tasks.SearchTasks;

/// <summary>
/// Handles searching for tasks.
/// </summary>
/// <param name="context">The context to access the database.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class SearchTasksHandler(IContext context, ICurrentSession session) : IRequestHandler<SearchTasksRequest, Result<PaginatedList<TaskResponse>>>
{
    /// <summary>
    /// Handles the request to search for tasks.
    /// </summary>
    /// <param name="request">The request to search for tasks.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<PaginatedList<TaskResponse>>> Handle(SearchTasksRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (!project.ProjectMembers.Any(x => x.UserId == session.UserId) && !project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You don't have permission to view tasks in this project.");

        var tasks = context
            .ProjectTasks.Where(x => x.ProjectId == request.ProjectId)
            .WhereIf(!string.IsNullOrEmpty(request.Title), x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Title!.ToLower().Trim()}%"))
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status))
            .WhereIf(request.Priorities.Any(), x => request.Priorities.Contains(x.Priority));

        return await tasks.ProjectToType<TaskResponse>().OrderBy(x => x.Title).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
