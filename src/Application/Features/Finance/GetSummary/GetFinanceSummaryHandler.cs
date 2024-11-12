using System.Threading;
using System.Threading.Tasks;
using Platform.Application.Common;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Features.Finance.GetSummary;
using Platform.Domain.Auth.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Application.Features.Finance.GetSummary;

/// <summary>
/// Handler for getting finance summary for a workspace
/// </summary>
public class GetFinanceSummaryHandler : IRequestHandler<GetFinanceSummaryRequest, Result<FinanceSummaryResponse>>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;
    /// <summary>
    /// Constructor for GetFinanceSummaryHandler
    /// </summary>
    /// <param name="context">The database context</param>
    /// <param name="session">The current session</param>
    public GetFinanceSummaryHandler(IContext context, ICurrentSession session)
    {
        _context = context;
        _session = session;
    }

    /// <summary>
    /// Handles the request to get finance summary for a workspace
    /// </summary>
    /// <param name="request">The request containing filter and pagination parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing a FinanceSummaryResponse object.</returns>
    public async Task<Result<FinanceSummaryResponse>> Handle(
        GetFinanceSummaryRequest request,
        CancellationToken cancellationToken)
    {
        // Verify workspace membership
        var workspaceMember = await _context.WorkspaceMembers
            .FirstOrDefaultAsync(wm =>
                wm.WorkspaceId == request.WorkspaceId &&
                wm.UserId == _session.UserId,
                cancellationToken);

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");

        // Build query with filters
        var query = _context.ProjectTasks
            .Where(t => t.WorkspaceId == request.WorkspaceId);

        // Apply project filter if specified
        if (request.ProjectId != null)
            query = query.Where(t => t.ProjectId == request.ProjectId);

        // Apply task status filter if specified
        if (request.IsClosed.HasValue)
            query = query.Where(t => t.IsClosed == request.IsClosed.Value);

        // Calculate total cost and task count
        var totalCost = await query.SumAsync(t => t.Cost ?? 0, cancellationToken);
        var taskCount = await query.CountAsync(cancellationToken);

        var response = new FinanceSummaryResponse
        {
            TotalCost = totalCost,
            TaskCount = taskCount
        };

        return Result.Success(response);
    }
}
