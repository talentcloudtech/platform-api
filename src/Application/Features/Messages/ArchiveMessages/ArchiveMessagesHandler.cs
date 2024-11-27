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
        var messageRecipients = await context.MessageRecipients
            .Where(mr => request.MessageIds.Contains(mr.MessageId) &&
                        mr.RecipientId == session.UserId)
            .ToListAsync(cancellationToken);

        if (!messageRecipients.Any())
            return Result.NotFound("No messages found or you don't have access to them.");

        foreach (var recipient in messageRecipients)
        {
            recipient.IsArchived = true;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}