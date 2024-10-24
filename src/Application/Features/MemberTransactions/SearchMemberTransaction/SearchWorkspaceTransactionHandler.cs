using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.MemberTransactions;
using Platform.Application.Features.MemberTransactions.SearchMemberTransaction;
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
public class SearchMemberTransactionsHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchMemberTransactionRequest, Result<PaginatedList<MemberTransactionResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspaces based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating workspaces.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of workspaces.</returns>
    public async Task<Result<PaginatedList<MemberTransactionResponse>>> Handle(
        SearchMemberTransactionRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<MemberTransaction> query = context.MemberTransactions;

        if (session.Role!.Name != UserRole.Admin.Name && session.Role!.Name != UserRole.Customer.Name)
            return Result.Forbidden("User does not have permission to create a member transaction.");

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => EF.Functions.Like(x.Description.ToLower().Trim(), $"%{request.Search!.ToLower().Trim()}%"));

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var memberTransactions = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<MemberTransactionResponse>
        {
            CurrentPage = memberTransactions.CurrentPage,
            TotalPages = memberTransactions.TotalPages,
            TotalItems = memberTransactions.TotalItems,
            Result = memberTransactions
                .Result.Select(x =>
                {
                    var memberTransactionResponse = x.Adapt<MemberTransactionResponse>();
                    return memberTransactionResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
