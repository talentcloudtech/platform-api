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
using Platform.Application.Features.Projects;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Tasks.SearchUsersTask;

/// <summary>
/// Handles searching for tasks.
/// </summary>
/// <param name="context">The context to access the database.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the operation.</returns>
public class SearchUsersTaskHandler(IContext context, ICurrentSession session) : IRequestHandler<SearchUsersTaskRequest, Result<PaginatedList<TaskResponse>>>
{
    /// <summary>
    /// Handles the request to search for tasks.
    /// </summary>
    /// <param name="request">The request to search for tasks.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<PaginatedList<TaskResponse>>> Handle(SearchUsersTaskRequest request, CancellationToken cancellationToken)
    {
        IQueryable<ProjectTask> query = context.ProjectTasks.Include(i => i.Workspace).Include(i => i.Project).Include(i => i.User.MemberProfile);

        // First filter by project membership
        // Allow access to open tasks or tasks where user is a project member
        query = query.Where(x =>
            x.Type == TaskType.Open
            || // Allow access to open tasks
            x.Project.ProjectMembers.Any(pm => pm.UserId == session.UserId)
        );

        //Apply workspace and project filter
        // Filter tasks where user is either creator or assignee
        query = query.Where(x =>
            x.User.Id == session.UserId
            || // Created by user
            x.Assignments.Any(a => a.User.Id == session.UserId) // Assigned to user
        );

        //Apply workspace filter
        if (request.WorkspaceIds!.Any())
            query = query.Where(i => request.WorkspaceIds!.Contains(i.WorkspaceId));

        //Apply project filter
        if (request.ProjectIds!.Any())
            query = query.Where(i => request.ProjectIds!.Contains(i.ProjectId));

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply status filter
        if (request.Statuses!.Any())
            query = query.Where(x => request.Statuses!.Contains(x.Status));

        //Apply priorities filter
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
                    taskResponse.Assignees = context
                        .ProjectTaskAssignments.Where(a => a.TaskId == x.Id)
                        .Select(a => new CommonUserResponse(a.User, a.User.MemberProfile))
                        .ToList();
                    return taskResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
