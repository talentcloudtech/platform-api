using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Messages.MarkMessageRead;

/// <summary>
/// Handles marking a message as read
/// </summary>
public class MarkMessageReadHandler(IContext context, ICurrentSession session) : IRequestHandler<MarkMessageReadRequest, Result>
{
    /// <summary>
    /// Handles the request to mark a message as read
    /// </summary>
    /// <param name="request">The request containing the message ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A result indicating success or failure</returns>
    public async Task<Result> Handle(MarkMessageReadRequest request, CancellationToken cancellationToken)
    {
        // Find the message recipient record
        var messageRecipient = await context.MessageRecipients.FirstOrDefaultAsync(
            mr => mr.MessageId == request.MessageId && mr.RecipientId == session.UserId,
            cancellationToken
        );

        if (messageRecipient == null)
            return Result.NotFound("Message not found or you don't have access to it.");

        // If already read, return success
        if (messageRecipient.IsRead)
            return Result.Success();

        // Update the read status
        messageRecipient.IsRead = true;
        messageRecipient.ReadAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
