using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.WebhookProcessors.LogtoWebhook;

/// <summary>
/// Represents a request to create a member profile.
/// </summary>
public record LogtoWebhookRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the raw body of the request.
    /// </summary>
    public string? RawBody { get; init; }

    /// <summary>
    /// Gets or sets the user entity from Logto.
    /// </summary>
    public string? Signature { get; init; }
}
