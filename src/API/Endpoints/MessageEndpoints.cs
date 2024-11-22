using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Domain.Entities.Common;
using MediatR;
using System.Threading;
using Platform.Application.Extensions;

namespace Platform.API.Endpoints;

/// <summary>
/// Defines the message-related endpoints
/// </summary>
public class MessageEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the message endpoints to the application
    /// </summary>
    /// <param name="app">The application builder</param>
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var messages = app.MapGroup("/messages").WithTags("Message").RequireAuthorization();

        messages
            .MapGet(
                "/",
                async (
                    [AsParameters] SearchMessagesRequest request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Search inbox messages");


        // GET /messages/sent - Search sent messages
        messages.MapGet("/sent", async (
            [AsParameters] SearchSentMessagesRequest request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Search sent messages");

        // POST /messages/send - Send a new message
        messages.MapPost("/send", async (
            SendMessageRequest request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Send a message");

        // POST /messages/read - Mark message as read
        messages.MapPost("/read", async (
            MarkMessageReadRequest request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Mark message as read");

        // GET /messages/{messageId}/replies - Get message replies
        messages.MapGet("/{messageId}/replies", async (
            MessageId messageId,
            int pageSize,
            int page,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            var request = new GetMessageRepliesRequest(messageId, page, pageSize);
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Get message replies");

        // POST /messages/{messageId}/reply - Reply to a message
        messages.MapPost("/{messageId}/reply", async (
            MessageId messageId,
            ReplyToMessageRequest request,
            ISender sender,
            CancellationToken cancellationToken
        ) =>
        {
            request = request with { MessageId = messageId };
            var result = await sender.Send(request, cancellationToken);
            return result.ToPlatformApiResult();
        })
        .WithSummary("Reply to a message");
    }
}
