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
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

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
        IQueryable<ProjectTask> query = context
            .ProjectTasks.Include(x => x.Assignments)
            .Include(i => i.Workspace)
            .Include(i => i.Project)
            .Include(i => i.User.MemberProfile);

        query = query.Where(x => x.WorkspaceId == request.WorkspaceId && x.ProjectId == request.ProjectId);

        //Apply workspace and project filter
        query = query.Where(x =>
            x.Workspace.WorkspaceMembers.Any(m =>
                m.UserId == session.UserId && (m.Role == WorkspaceRole.WorkspaceOwner || m.Role == WorkspaceRole.WorkspaceManager)
            ) || x.Project.ProjectMembers.Any(m => m.UserId == session.UserId)
        );

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply status filter
        if (request.Statuses!.Any())
            query = query.Where(x => request.Statuses!.Contains(x.Status));

        // Apply priority filter
        if (request.Priorities!.Any())
            query = query.Where(x => request.Priorities!.Contains(x.Priority));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

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
                    return taskResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
