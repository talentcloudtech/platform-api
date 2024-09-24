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
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Tasks.SearchTasks;

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
        var tasks = context
            .ProjectTasks.Where(x => x.Workspace.Members.Any(m => m.UserId == session.UserId))
            .WhereIf(request.WorkspaceIds.Any(), x => request.WorkspaceIds.Contains(x.WorkspaceId))
            .WhereIf(request.ProjectIds.Any(), x => request.ProjectIds.Contains(x.ProjectId))
            .WhereIf(!string.IsNullOrEmpty(request.Title), x => EF.Functions.Like(x.Title, $"%{request.Title}%"))
            .WhereIf(request.Statuses.Any(), x => request.Statuses.Contains(x.Status))
            .WhereIf(request.Priorities.Any(), x => request.Priorities.Contains(x.Priority));

        return await tasks.ProjectToType<TaskResponse>().OrderBy(x => x.Title).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
