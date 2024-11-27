using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

/// <summary>
/// Handles un archiving messages
/// </summary>
/// <param name="context">The database context</param>
/// <param name="session">The current session</param>
public class UnarchiveMessagesHandler(IContext context, ICurrentSession session)
    : IRequestHandler<UnarchiveMessagesRequest, Result>
{
    /// <summary>
    /// Handles the request to unarchive messages
    /// </summary>
    public async Task<Result> Handle(UnarchiveMessagesRequest request, CancellationToken cancellationToken)
    {
        var messageRecipients = await context.MessageRecipients
            .Where(mr => request.MessageIds.Contains(mr.MessageId) &&
                        mr.RecipientId == session.UserId)
            .ToListAsync(cancellationToken);

        if (!messageRecipients.Any())
            return Result.NotFound("No messages found or you don't have access to them.");

        foreach (var recipient in messageRecipients)
        {
            recipient.IsArchived = false;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}