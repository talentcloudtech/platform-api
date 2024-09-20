using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a message recipient entity in the system.
/// </summary>
public class MessageRecipient : Entity<MessageRecipientId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the message recipient.
    /// </summary>
    public override MessageRecipientId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the message identifier associated with this recipient.
    /// </summary>
    public MessageId MessageId { get; set; }

    /// <summary>
    /// Gets or sets the recipient's user identifier.
    /// </summary>
    public UserId RecipientId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message has been read by the recipient.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets the date when the message was read by the recipient.
    /// </summary>
    public DateTime? ReadAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message has been archived by the recipient.
    /// </summary>
    public bool IsArchived { get; set; }

    /// <summary>
    /// Gets or sets the date when the recipient received the message.
    /// </summary>
    public DateTime ReceivedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message has been muted by the recipient.
    /// </summary>
    public bool IsMuted { get; set; }

    /// <summary>
    /// Gets or sets the date when the message was muted by the recipient.
    /// </summary>
    public DateTime? MutedAt { get; set; }

    /// <summary>
    /// Gets or sets the message associated with this recipient.
    /// </summary>
    public Message Message { get; set; } = null!;
}