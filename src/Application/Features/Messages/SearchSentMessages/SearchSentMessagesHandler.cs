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

namespace Platform.Application.Features.Messages.SearchSentMessages;

/// <summary>
/// Handles searching for messages sent by the current user
/// </summary>
public class SearchSentMessagesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchSentMessagesRequest, Result<PaginatedList<MessageResponse>>>
{
    /// <summary>
    /// Handles the request to search for messages sent by the current user
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating messages.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of messages.</returns>
    public async Task<Result<PaginatedList<MessageResponse>>> Handle(SearchSentMessagesRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Message> query = context.Messages.Include(m => m.Recipients);

        // Filter messages sent by current user
        query = query.Where(m => m.SenderId == session.UserId && !m.IsArchived);

        // Apply workspace filter
        if (request.WorkspaceId != null)
            query = query.Where(m => m.WorkspaceId == request.WorkspaceId);

        // Apply project filter
        if (request.ProjectId != null)
            query = query.Where(m => m.ProjectId == request.ProjectId);

        // Apply search filter
        if (!string.IsNullOrEmpty(request.SearchText))
            query = query.Where(m =>
                EF.Functions.Like(m.Subject.ToLower(), $"%{request.SearchText.ToLower()}%")
                || EF.Functions.Like(m.Body.ToLower(), $"%{request.SearchText.ToLower()}%")
            );

        // Apply message type filter
        if (request.MessageTypes?.Any() == true)
            query = query.Where(m => request.MessageTypes.Contains(m.MessageType));

        // Apply date range filter
        if (request.StartDate.HasValue)
            query = query.Where(m => m.CreatedAt >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(m => m.CreatedAt <= request.EndDate.Value);

        // Apply sorting
        if (request.SortOrder.ToLower() == "asc")
            query = query.OrderBy(i => EF.Property<object>(i, request.SortBy));
        else
            query = query.OrderByDescending(i => EF.Property<object>(i, request.SortBy));

        var messages = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var senders = await context
            .Users.Include(u => u.MemberProfile)
            .Where(u => messages.Result.Select(m => m.SenderId).Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u, cancellationToken);

        var recipientIds = await context
            .MessageRecipients.Where(mr => messages.Result.Select(m => m.Id).Contains(mr.MessageId))
            .Select(mr => mr.RecipientId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var recipients = await context
            .Users.Include(u => u.MemberProfile)
            .Where(u => recipientIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u, cancellationToken);

        var paginatedResponse = new PaginatedList<MessageResponse>
        {
            CurrentPage = messages.CurrentPage,
            TotalPages = messages.TotalPages,
            TotalItems = messages.TotalItems,
            Result = messages
                .Result.Select(m =>
                {
                    var messageResponse = m.Adapt<MessageResponse>();
                    messageResponse.Sender = new CommonUserResponse(senders[m.SenderId], senders[m.SenderId].MemberProfile);
                    messageResponse.Recipients = m
                        .Recipients.Select(r => new CommonUserResponse(recipients[r.RecipientId], recipients[r.RecipientId].MemberProfile))
                        .ToList();
                    return messageResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
