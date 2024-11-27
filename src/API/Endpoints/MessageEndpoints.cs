using System.Threading;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Domain.Entities.Common;

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
        var messages = app.MapGroup("api/messages").WithTags("Message").RequireAuthorization();

        messages
            .MapGet(
                "/",
                async ([AsParameters] SearchMessagesRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Search inbox messages");

        // GET /messages/sent - Search sent messages
        messages
            .MapGet(
                "/sent",
                async ([AsParameters] SearchSentMessagesRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Search sent messages");

        // POST /messages/send - Send a new message
        messages
            .MapPost(
                "/send",
                async (SendMessageRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Send a message");

        // POST /messages/read - Mark messages as read
        messages
            .MapPost(
                "/read",
                async (MarkMessageReadRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Mark message as read");

        // GET /messages/{messageId}/replies - Get message replies
        messages
            .MapGet(
                "/{messageId}/replies",
                async (MessageId messageId, [AsParameters] GetMessageRepliesRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    request = request with { MessageId = messageId };
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get message replies");

        // POST /messages/{messageId}/reply - Reply to a message
        messages
            .MapPost(
                "/{messageId}/reply",
                async (MessageId messageId, ReplyToMessageRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    request = request with { MessageId = messageId };
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Reply to a message");

        // POST /messages/archive - Archive messages
        messages
            .MapPost(
                "/archive",
                async (ArchiveMessagesRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Archive messages");

        // POST /messages/unarchive - Unarchive messages
        messages
            .MapPost(
                "/unarchive",
                async (UnarchiveMessagesRequest request, ISender sender, CancellationToken cancellationToken) =>
                {
                    var result = await sender.Send(request, cancellationToken);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Unarchive messages");
    }
}
