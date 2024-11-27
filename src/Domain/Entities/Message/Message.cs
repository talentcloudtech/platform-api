using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a message entity in the system.
/// </summary>
public class Message : Entity<MessageId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the message.
    /// </summary>
    public override MessageId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the sender's user identifier.
    /// </summary>
    public UserId SenderId { get; set; }

    /// <summary>
    /// Gets or sets the project identifier associated with this message (if applicable).
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this message (if applicable).
    /// </summary>
    public WorkspaceId? WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the subject of the message.
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    /// Gets or sets the body of the message.
    /// </summary>
    public string Body { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of the message.
    /// </summary>
    public MessageType MessageType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether replies are allowed for this message.
    /// </summary>
    public bool ReplyAllowed { get; set; }

    /// <summary>
    /// Gets or sets the date when the message was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the total number of recipients for this message.
    /// </summary>
    public int RecipientCount { get; set; }

    /// <summary>
    /// Gets or sets the total number of replies to this message.
    /// </summary>
    public int TotalReplyCount { get; set; }

    /// <summary>
    /// Gets or sets the sender of the message.
    /// </summary>
    public ApplicationUser? Sender { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message has been archived by the recipient.
    /// </summary>
    public bool IsArchived { get; set; }


    /// <summary>
    /// Gets or sets the collection of message recipients.
    /// </summary>
    public ICollection<MessageRecipient> Recipients { get; set; } = new List<MessageRecipient>();
}
