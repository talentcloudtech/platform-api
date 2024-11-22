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

namespace Platform.Application.Features.Messages.SearchMessages;

/// <summary>
/// Handles searching for messages in user's inbox
/// </summary>
public class SearchMessagesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchMessagesRequest, Result<PaginatedList<MessageResponse>>>
{
    /// <summary>
    /// Handles the request to search for messages based on the provided filtering and pagination options.
    /// </summary>
    /// <param name="request">The search criteria for filtering and paginating messages.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A paginated list of messages.</returns>
    public async Task<Result<PaginatedList<MessageResponse>>> Handle(SearchMessagesRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Message> query = context.Messages
            .Include(m => m.Recipients)
            .Include(m => m.Sender);

        // Filter messages where current user is a recipient
        query = query.Where(m => m.Recipients.Any(r => r.RecipientId == session.UserId));

        // Apply workspace filter
        if (request.WorkspaceId != null)
            query = query.Where(m => m.WorkspaceId == request.WorkspaceId);

        // Apply project filter
        if (request.ProjectId != null)
            query = query.Where(m => m.ProjectId == request.ProjectId);

        // Apply archived filter
        if (request.Archived.HasValue)
            query = query.Where(m => m.Recipients
                .Any(r => r.RecipientId == session.UserId && r.IsArchived == request.Archived.Value));

        // Apply unread filter
        if (request.Unread.HasValue)
            query = query.Where(m => m.Recipients
                .Any(r => r.RecipientId == session.UserId && !r.IsRead == request.Unread.Value));

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(m =>
                EF.Functions.Like(m.Subject.ToLower(), $"%{request.Search.ToLower()}%") ||
                EF.Functions.Like(m.Body.ToLower(), $"%{request.Search.ToLower()}%"));

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

        // Get sender details in a separate query to avoid navigation property issues
        var senderIds = messages.Result.Select(m => m.SenderId).Distinct().ToList();
        var senders = await context.Users
            .Include(u => u.MemberProfile)
            .Where(u => senderIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u, cancellationToken);

        var paginatedResponse = new PaginatedList<MessageResponse>
        {
            CurrentPage = messages.CurrentPage,
            TotalPages = messages.TotalPages,
            TotalItems = messages.TotalItems,
            Result = messages.Result.Select(m =>
            {
                var messageResponse = m.Adapt<MessageResponse>();
                messageResponse.Sender = m.Sender != null ? new CommonUserResponse(m.Sender, m.Sender.MemberProfile) : new CommonUserResponse();
                messageResponse.IsRead = m.Recipients.First(r => r.RecipientId == session.UserId).IsRead;
                messageResponse.IsArchived = m.Recipients.First(r => r.RecipientId == session.UserId).IsArchived;
                return messageResponse;
            }).ToList()
        };

        return Result.Success(paginatedResponse);
    }
}