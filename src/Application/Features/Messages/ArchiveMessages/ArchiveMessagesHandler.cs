using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;


/// <summary>
/// Handles archiving multiple messages
/// </summary>
/// <param name="context">The database context</param>
/// <param name="session">The current session</param>

public class ArchiveMessagesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<ArchiveMessagesRequest, Result>
{
    /// <summary>
    /// Handles the request to archive messages
    /// </summary>
    public async Task<Result> Handle(ArchiveMessagesRequest request, CancellationToken cancellationToken)
    {
        // Get messages where user is recipient
        var recipientMessages = await context.MessageRecipients
            .Where(mr => request.MessageIds.Contains(mr.MessageId) &&
                        mr.RecipientId == session.UserId)
            .ToListAsync(cancellationToken);

        // Get messages where user is sender
        var sentMessages = await context.Messages
            .Where(m => request.MessageIds.Contains(m.Id) &&
                        m.SenderId == session.UserId)
            .ToListAsync(cancellationToken);

        if (!recipientMessages.Any() && !sentMessages.Any())
            return Result.NotFound("No messages found or you don't have access to them.");

        // Archive received messages
        foreach (var message in recipientMessages)
        {
            message.IsArchived = true;
        }

        // Archive sent messages
        foreach (var message in sentMessages)
        {
            message.IsArchived = true; // Assuming you have this field in your Message entity
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}