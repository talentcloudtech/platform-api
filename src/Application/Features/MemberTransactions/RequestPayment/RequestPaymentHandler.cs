using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Domain.Entities.Enums;
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

namespace Platform.Application.Features.MemberTransactions.RequestPayment;

/// <summary>
/// Handles the request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>A paginated list of workspaces.</returns>
public class RequestPaymentHandler(IContext context, ICurrentSession session)
    : IRequestHandler<RequestPaymentRequest, Result<PaginatedList<MemberTransactionResponse>>>
{
    /// <summary>
    /// Handles the request to search for workspaces based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating workspaces.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of workspaces.</returns>
    public async Task<Result<PaginatedList<MemberTransactionResponse>>> Handle(RequestPaymentRequest request, CancellationToken cancellationToken)
    {
        IQueryable<MemberTransaction> query = context.MemberTransactions.Include(x => x.User).Include(x => x.User!.MemberProfile);

        if (session.Role!.Name != UserRole.Admin.Name)
            return Result.Forbidden("Admin and Customer does not have permission to search member transactions.");

        // Add filter for pending transactions
        query = query.Where(x => x.Status == TransactionStatus.Pending);

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
                    memberTransactionResponse.UserName = x.User?.UserName ?? "";
                    return memberTransactionResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
