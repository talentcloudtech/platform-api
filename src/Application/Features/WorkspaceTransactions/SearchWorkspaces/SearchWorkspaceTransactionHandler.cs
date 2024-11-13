using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceTransactions.SearchWorkspaceTransactions;

/// <summary>
/// Handles the request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>A paginated list of workspaces.</returns>
public class SearchWorkspaceTransactionsHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchWorkspaceTransactionRequest, Result<PaginatedList<WorkspaceTransactionResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspaces based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating workspaces.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of workspaces.</returns>
    public async Task<Result<PaginatedList<WorkspaceTransactionResponse>>> Handle(
        SearchWorkspaceTransactionRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<WorkspaceTransaction> query = context.WorkspaceTransactions;

        var workspaceMember = await context.WorkspaceMembers
            .FirstOrDefaultAsync(wm => wm.WorkspaceId == request.WorkspaceId && wm.UserId == session.UserId, cancellationToken);

        if (workspaceMember == null)
            return Result.Forbidden("You are not a member of this workspace.");
        if (workspaceMember.Role != WorkspaceRole.WorkspaceOwner && session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("You don't have permission to access workspace transactions.");

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Description.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var workspaceTransactions = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceTransactionResponse>
        {
            CurrentPage = workspaceTransactions.CurrentPage,
            TotalPages = workspaceTransactions.TotalPages,
            TotalItems = workspaceTransactions.TotalItems,
            Result = workspaceTransactions
                .Result.Select(x =>
                {
                    var workspaceTransactionResponse = x.Adapt<WorkspaceTransactionResponse>();
                    return workspaceTransactionResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
