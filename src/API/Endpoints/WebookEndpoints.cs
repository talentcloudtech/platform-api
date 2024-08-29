using System.IO;
using System.Linq;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Features.WebhookProcessors.LogtoWebhook;

namespace Platform.Api.Endpoints;

/// <summary>
/// Represents the endpoints for webhooks.
/// </summary>
public static class WebhookEndpoints
{
    /// <summary>
    /// Maps the endpoints for webhooks.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapWebhookEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/webhooks").WithTags("Webhook").AllowAnonymous();

        group
            .MapPost(
                "/logto",
                async (IMediator mediator, HttpContext context) =>
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var rawBody = await reader.ReadToEndAsync();

                    var signature = context.Request.Headers["logto-signature-sha-256"].FirstOrDefault();

                    var result = await mediator.Send(new LogtoWebhookRequest() with { RawBody = rawBody, Signature = signature });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Logto webhook");
    }
}
