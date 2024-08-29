using System.IO;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Platform.Application.Features.Accounts.CreateAccount;

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
                async (
                    IMediator mediator,
                    [FromHeader(Name = "logto-signature-sha-256")] string signature,
                    [FromBody] LogtoWebhookRequest body
                ) =>
                {
                    return await mediator.Send(
                        new CreateAccountRequest() with
                        {
                            LogtoWebhookRequest = body,
                            Signature = signature
                        }
                    );
                }
            )
            .WithSummary("Logto webhook");
    }
}
