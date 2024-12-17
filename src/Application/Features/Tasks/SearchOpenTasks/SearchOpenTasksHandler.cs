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
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.SearchOpenTasks;

/// <summary>
/// Handles the search for open tasks.
/// </summary>
/// <param name="context"></param>
public class SearchOpenTasksHandler(IContext context) : IRequestHandler<SearchOpenTasksRequest, Result<PaginatedList<TaskResponse>>>
{
    /// <summary>
    /// Handles the search for open tasks.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PaginatedList<TaskResponse>>> Handle(SearchOpenTasksRequest request, CancellationToken cancellationToken)
    {
        var query = context
            .ProjectTasks.Include(x => x.Assignments)
            .ThenInclude(x => x.User.MemberProfile)
            .Include(i => i.Workspace)
            .Include(i => i.Project)
            .Include(i => i.User.MemberProfile)
            .Where(x => x.Type == TaskType.Open && !x.IsClosed); // Filter for open tasks only

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%"));

        // Modified IsPublished filter
        if (request.IsPublished.HasValue)
            query = query.Where(x => x.IsPublished == request.IsPublished.Value);

        // Apply priority filter
        if (request.Priorities?.Any() == true)
            query = query.Where(x => request.Priorities.Contains(x.Priority));

        // Apply sorting
        query =
            request.SortOrder.ToLower() == "asc"
                ? query.OrderBy(i => EF.Property<object>(i, request.SortBy))
                : query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var tasks = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<TaskResponse>
        {
            CurrentPage = tasks.CurrentPage,
            TotalPages = tasks.TotalPages,
            TotalItems = tasks.TotalItems,
            Result = tasks
                .Result.Select(x =>
                {
                    var taskResponse = x.Adapt<TaskResponse>();
                    taskResponse.CreatedByUser = new CommonUserResponse(x.User, x.User.MemberProfile);
                    taskResponse.Workspace = x.Workspace.Adapt<WorkspaceResponse>();
                    taskResponse.Project = x.Project.Adapt<ProjectResponse>();
                    taskResponse.Assignees = x.Assignments.Select(a => new CommonUserResponse(a.User, a.User.MemberProfile)).ToList();
                    return taskResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
