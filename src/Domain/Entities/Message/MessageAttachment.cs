using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an attachment to a message in the system.
/// </summary>
public class MessageAttachment : Entity<MessageAttachmentId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the message attachment.
    /// </summary>
    public override MessageAttachmentId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the identifier of the message this attachment belongs to.
    /// </summary>
    public MessageId MessageId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the reply this attachment belongs to (if applicable).
    /// </summary>
    public MessageReplyId? ReplyId { get; set; }

    /// <summary>
    /// Gets or sets the original name of the file uploaded.
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Azure Blob URL (without the SAS token).
    /// </summary>
    public string FileUrl { get; set; } = null!;

    /// <summary>
    /// Gets or sets the MIME type of the file.
    /// </summary>
    public string FileType { get; set; } = null!;

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the date when the attachment was uploaded.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who uploaded the attachment.
    /// </summary>
    public UserId UploadedBy { get; set; }

    /// <summary>
    /// Gets or sets the project identifier associated with this attachment (if applicable).
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this attachment (if applicable).
    /// </summary>
    public WorkspaceId? WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the message associated with this attachment.
    /// </summary>
    public Message Message { get; set; } = null!;

    /// <summary>
    /// Gets or sets the reply associated with this attachment (if applicable).
    /// </summary>
    public MessageReply? Reply { get; set; }
}
