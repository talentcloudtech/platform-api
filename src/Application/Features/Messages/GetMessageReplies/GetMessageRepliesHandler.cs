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

namespace Platform.Application.Features.Messages.GetMessageReplies;

/// <summary>
/// Handles retrieving replies for a specific message
/// </summary>
public class GetMessageRepliesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetMessageRepliesRequest, Result<PaginatedList<MessageReplyResponse>>>
{
    /// <summary>
    /// Handles the request to get message replies
    /// </summary>
    /// <param name="request">The request containing message ID and pagination parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of message replies</returns>
    public async Task<Result<PaginatedList<MessageReplyResponse>>> Handle(GetMessageRepliesRequest request, CancellationToken cancellationToken)
    {
        // First verify the message exists and user has access to it
        var message = await context.Messages.Include(m => m.Recipients).FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

        if (message == null)
            return Result.NotFound("Message not found.");

        // Check if user has access to the message (is sender or recipient)
        var hasAccess = message.SenderId == session.UserId || message.Recipients.Any(r => r.RecipientId == session.UserId);

        if (!hasAccess)
            return Result.Forbidden("You don't have permission to view this message's replies.");

        // Query replies
        var query = context.MessageReplies.Where(r => r.OriginalMessageId == request.MessageId);

        // Apply sorting by creation date
        query = query.OrderBy(r => r.CreatedAt);

        var replies = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        // Get sender details in a separate query
        var senderIds = replies.Result.Select(r => r.SenderId).Distinct().ToList();
        var senders = await context
            .Users.Include(u => u.MemberProfile)
            .Where(u => senderIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u, cancellationToken);

        var paginatedResponse = new PaginatedList<MessageReplyResponse>
        {
            CurrentPage = replies.CurrentPage,
            TotalPages = replies.TotalPages,
            TotalItems = replies.TotalItems,
            Result = replies
                .Result.Select(r =>
                {
                    var replyResponse = r.Adapt<MessageReplyResponse>();
                    if (senders.TryGetValue(r.SenderId, out var sender))
                        replyResponse.Sender = new CommonUserResponse(sender, sender.MemberProfile);
                    else
                        replyResponse.Sender = new CommonUserResponse();

                    return replyResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
