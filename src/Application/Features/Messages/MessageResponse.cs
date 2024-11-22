using Platform.Domain.Entities.Enums;
using Platform.Domain.Entities.Common;
using System;
using Platform.Application.Common.Responses;
using System.Collections.Generic;


/// <summary>
/// Represents the response model for a message.
/// </summary>
public class MessageResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the message.
    /// </summary>
    public required MessageId Id { get; set; }

    /// <summary>
    /// Gets or sets the subject of the message.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// Gets or sets the body of the message.
    /// </summary>
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the type of the message.
    /// </summary>
    public required MessageType MessageType { get; set; }

    /// <summary>
    /// Gets or sets the creation date and time of the message.
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the sender of the message.
    /// </summary>
    public required CommonUserResponse Sender { get; set; }

    /// <summary>
    /// Gets or sets whether the message has been read.
    /// </summary>
    public required bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets whether the message is archived.
    /// </summary>
    public required bool IsArchived { get; set; }

    /// <summary>
    /// Gets or sets the attachments of the message.
    /// </summary>
    public List<MessageAttachmentResponse> Attachments { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of replies to the message.
    /// </summary>
    public required int TotalReplyCount { get; set; }

    /// <summary>
    /// Gets or sets the replies to the message.
    /// </summary>
    public List<MessageReplyResponse>? Replies { get; set; }
}

/// <summary>
/// Represents the response model for a message reply.
/// </summary>
public class MessageReplyResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the message reply.
    /// </summary>
    public required MessageReplyId Id { get; set; }

    /// <summary>
    /// Gets or sets the body of the message reply.
    /// </summary>
    public required string Body { get; set; }

    /// <summary>
    /// Gets or sets the creation date and time of the message reply.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the sender of the message reply.
    /// </summary>
    public required CommonUserResponse Sender { get; set; }

    /// <summary>
    /// Gets or sets the attachments of the message reply.
    /// </summary>
    public List<MessageAttachmentResponse> Attachments { get; set; } = [];
}

/// <summary>
/// Represents the response model for a message attachment.
/// </summary>
public class MessageAttachmentResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the message attachment.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    public required string FileName { get; set; }
}