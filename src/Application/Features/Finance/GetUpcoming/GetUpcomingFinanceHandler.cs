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

namespace Platform.Application.Features.Finance.GetUpcoming;

/// <summary>
/// Handler for getting upcoming finance details
/// </summary>
public class GetUpcomingFinanceHandler : IRequestHandler<GetUpcomingFinanceRequest, Result<PaginatedList<UpcomingFinanceResponse>>>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUpcomingFinanceHandler"/> class
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="session">The current session</param>
    public GetUpcomingFinanceHandler(IContext context, ICurrentSession session)
    {
        _context = context;
        _session = session;
    }

    /// <summary>
    /// Handles the request to get upcoming finance details
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result<PaginatedList<UpcomingFinanceResponse>>> Handle(GetUpcomingFinanceRequest request, CancellationToken cancellationToken)
    {
        // Verify workspace membership
        var workspaceMember = await _context.WorkspaceMembers.FirstOrDefaultAsync(
            wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == _session.UserId,
            cancellationToken
        );

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");

        // Build query with filters
        var query = _context
            .ProjectTasks.Where(t => t.WorkspaceId == request.WorkspaceId && !t.IsClosed && t.Cost > 0);

        // Apply project filter if specified
        if (request.ProjectId != null)
            query = query.Where(t => t.ProjectId == request.ProjectId);

        // Apply status filter if specified
        if (request.Status.HasValue)
            query = query.Where(t => t.Status == request.Status.Value);

        // Apply search filter if specified
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Title.ToLower().Trim(), $"%{request.Search.ToLower().Trim()}%"));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var tasks = await query
            .Select(t => new UpcomingFinanceResponse
            {
                TaskId = t.Id,
                TaskTitle = t.Title,
                Status = t.Status,
                CompletionDate = t.DueDate,
                Cost = t.Cost,
                Assignees = t.Assignments.Select(a => new CommonUserResponse(a.User, a.User.MemberProfile)).ToList(),
            })
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<UpcomingFinanceResponse>
        {
            CurrentPage = tasks.CurrentPage,
            TotalPages = tasks.TotalPages,
            TotalItems = tasks.TotalItems,
            Result = tasks.Result,
        };

        return Result.Success(paginatedResponse);
    }
}
