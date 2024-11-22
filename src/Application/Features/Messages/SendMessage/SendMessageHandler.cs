using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Messages.SendMessage;

/// <summary>
/// Handles sending a new message
/// </summary>
public class SendMessageHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SendMessageRequest, Result<MessageResponse>>
{
    /// <summary>
    /// Handles the send message request
    /// </summary>
    /// <param name="request">The request to send a message.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The response containing the sent message.</returns>
    public async Task<Result<MessageResponse>> Handle(SendMessageRequest request, CancellationToken cancellationToken)
    {
        var message = new Message
        {
            Subject = request.Subject,
            Body = request.Body,
            MessageType = request.MessageType,
            SenderId = session.UserId,
            WorkspaceId = request.WorkspaceId,
            ProjectId = request.ProjectId,
            ReplyAllowed = request.ReplyAllowed,
            CreatedAt = DateTime.UtcNow,
            RecipientCount = request.RecipientIds.Count
        };

        // Create message recipients
        foreach (var recipientId in request.RecipientIds)
        {
            message.Recipients.Add(new MessageRecipient
            {
                RecipientId = recipientId,
                IsRead = false,
                IsArchived = false,
                ReceivedAt = DateTime.UtcNow
            });
        }

        context.Messages.Add(message);
        await context.SaveChangesAsync(cancellationToken);

        var messageResponse = message.Adapt<MessageResponse>();

        return Result.Success(messageResponse);
    }
}