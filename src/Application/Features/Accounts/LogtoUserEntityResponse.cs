using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a user entity as returned by Logto.
/// </summary>
public class LogtoUserEntityResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the primary email of the user.
    /// </summary>
    public string? PrimaryEmail { get; set; }

    /// <summary>
    /// Gets or sets the primary phone number of the user.
    /// </summary>
    public string? PrimaryPhone { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the avatar URL of the user.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets custom data associated with the user.
    /// </summary>
    public object? CustomData { get; set; }

    /// <summary>
    /// Gets or sets the identities associated with the user.
    /// </summary>
    public object? Identities { get; set; }

    // /// <summary>
    // /// Gets or sets the last sign-in time of the user.
    // /// </summary>
    // [JsonConverter(typeof(UnixTimestampConverter))]
    // public DateTime? LastSignInAt { get; set; }

    // /// <summary>
    // /// Gets or sets the creation time of the user.
    // /// </summary>
    // public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the application ID associated with the user.
    /// </summary>
    public string? ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is suspended.
    /// </summary>
    public bool? IsSuspended { get; set; }
}

/// <summary>
/// Represents a Logto webhook request.
/// </summary>
public class LogtoWebhookRequest
{
    /// <summary>
    /// Gets or sets the event type of the webhook.
    /// </summary>
    public required string Event { get; set; }

    /// <summary>
    /// Gets or sets the user ID associated with the webhook event.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the user data associated with the webhook event.
    /// </summary>
    public LogtoUserEntityResponse? User { get; set; }

    /// <summary>
    /// Gets or sets the time the event was triggered.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    // /// <summary>
    // /// Gets or sets the unique identifier for the webhook event.
    // /// </summary>
    // public required string EventId { get; set; }
}
