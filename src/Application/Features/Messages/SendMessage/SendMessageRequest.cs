using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;
using System.Collections.Generic;

/// <summary>
/// Request to send a new message
/// </summary>
public record SendMessageRequest : IRequest<Result<MessageResponse>>
{
    /// <summary>
    /// Gets or sets the subject of the message
    /// </summary>
    public required string Subject { get; init; }

    /// <summary>
    /// Gets or sets the body content of the message
    /// </summary>
    public required string Body { get; init; }

    /// <summary>
    /// Gets or sets the type of message
    /// </summary>
    public required MessageType MessageType { get; init; }

    /// <summary>
    /// Gets or sets the list of recipient user IDs
    /// </summary>
    public required List<UserId> RecipientIds { get; init; }

    /// <summary>
    /// Gets or sets the optional workspace ID associated with the message
    /// </summary>
    public WorkspaceId? WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the optional project ID associated with the message
    /// </summary>
    public ProjectId? ProjectId { get; init; }

    /// <summary>
    /// Gets or sets whether replies are allowed for this message
    /// </summary>
    public bool ReplyAllowed { get; init; } = true;

    /// <summary>
    /// Gets or sets the optional file attachments for the message
    /// </summary>
    public List<IFormFile>? Attachments { get; init; }
}