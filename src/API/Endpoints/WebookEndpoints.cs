using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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
    }
}
