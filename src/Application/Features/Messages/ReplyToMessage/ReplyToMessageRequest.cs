using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;

/// <summary>
/// Request to reply to an existing message
/// </summary>
public record ReplyToMessageRequest : IRequest<Result<MessageReplyResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the original message being replied to
    /// </summary>
    [JsonIgnore]
    public MessageId MessageId { get; init; }

    /// <summary>
    /// Gets or sets the body content of the reply
    /// </summary>
    public string Body { get; init; } = null!;

    /// <summary>
    /// Gets or sets the optional file attachments for the reply
    /// </summary>
    public List<IFormFile>? Attachments { get; init; }
}
