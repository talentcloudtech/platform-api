using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Messages.ReplyToMessage;

/// <summary>
/// Handles replying to a message
/// </summary>
public class ReplyToMessageHandler(IContext context, ICurrentSession session) : IRequestHandler<ReplyToMessageRequest, Result<MessageReplyResponse>>
{
    /// <summary>
    /// Handles the reply to message request
    /// </summary>
    /// <param name="request">The request to reply to a message.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response containing the reply to the message.</returns>
    public async Task<Result<MessageReplyResponse>> Handle(ReplyToMessageRequest request, CancellationToken cancellationToken)
    {
        // Get original message and verify it exists
        var originalMessage = await context.Messages.FirstOrDefaultAsync(m => m.Id == request.MessageId, cancellationToken);

        if (originalMessage == null)
            return Result.NotFound("Original message not found.");

        // Verify user can reply (is recipient or sender)
        var canReply =
            originalMessage.SenderId == session.UserId
            || await context.MessageRecipients.AnyAsync(r => r.MessageId == request.MessageId && r.RecipientId == session.UserId, cancellationToken);

        if (!canReply)
            return Result.Forbidden("You don't have permission to reply to this message.");

        if (!originalMessage.ReplyAllowed)
            return Result.Forbidden("Replies are not allowed for this message.");

        var reply = new MessageReply
        {
            OriginalMessageId = request.MessageId,
            SenderId = session.UserId,
            Body = request.Body,
            CreatedAt = DateTime.UtcNow,
        };

        context.MessageReplies.Add(reply);

        // Increment reply count on original message
        originalMessage.TotalReplyCount++;

        await context.SaveChangesAsync(cancellationToken);

        var senders = await context.Users.Include(u => u.MemberProfile).FirstOrDefaultAsync(u => u.Id == session.UserId, cancellationToken);

        var replyResponse = reply.Adapt<MessageReplyResponse>();

        return Result.Success(replyResponse);
    }
}
