using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a reply to a message in the system.
/// </summary>
public class MessageReply : Entity<MessageReplyId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the message reply.
    /// </summary>
    public override MessageReplyId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the identifier of the original message.
    /// </summary>
    public MessageId OriginalMessageId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who sent the reply.
    /// </summary>
    public UserId SenderId { get; set; }

    /// <summary>
    /// Gets or sets the body of the reply.
    /// </summary>
    public string Body { get; set; } = null!;

    /// <summary>
    /// Gets or sets the date when the reply was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the original message associated with this reply.
    /// </summary>
    public Message OriginalMessage { get; set; } = null!;
}
