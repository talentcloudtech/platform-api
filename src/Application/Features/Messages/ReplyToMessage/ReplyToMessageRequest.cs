using MediatR;
using Ardalis.Result;
using Platform.Domain.Entities.Common;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Request to reply to an existing message
/// </summary>
public record ReplyToMessageRequest : IRequest<Result<MessageReplyResponse>>
{
    /// <summary>
    /// Gets or sets the ID of the original message being replied to
    /// </summary>
    public required MessageId MessageId { get; init; }

    /// <summary>
    /// Gets or sets the body content of the reply
    /// </summary>
    public required string Body { get; init; }

    /// <summary>
    /// Gets or sets the optional file attachments for the reply
    /// </summary>
    public List<IFormFile>? Attachments { get; init; }
}