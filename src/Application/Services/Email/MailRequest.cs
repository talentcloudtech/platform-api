using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Platform.Application.Services.Email;

/// <summary>
/// Represents a request to send an email.
/// </summary>
public record MailRequest(
    Collection<string> to,
    string subject,
    string? body = null,
    string? from = null,
    string? displayName = null,
    string? replyTo = null,
    string? replyToName = null,
    Collection<string>? bcc = null,
    Collection<string>? cc = null,
    IDictionary<string, byte[]>? attachmentData = null,
    IDictionary<string, string>? headers = null
)
{
    /// <summary>
    /// Gets the collection of recipient email addresses.
    /// </summary>
    public Collection<string> To { get; } = to;

    /// <summary>
    /// Gets the subject of the email.
    /// </summary>
    public string Subject { get; } = subject;

    /// <summary>
    /// Gets the body content of the email.
    /// </summary>
    public string? Body { get; } = body;

    /// <summary>
    /// Gets the sender's email address.
    /// </summary>
    public string? From { get; } = from;

    /// <summary>
    /// Gets the display name of the sender.
    /// </summary>
    public string? DisplayName { get; } = displayName;

    /// <summary>
    /// Gets the reply-to email address.
    /// </summary>
    public string? ReplyTo { get; } = replyTo;

    /// <summary>
    /// Gets the name associated with the reply-to email address.
    /// </summary>
    public string? ReplyToName { get; } = replyToName;

    /// <summary>
    /// Gets the collection of BCC (Blind Carbon Copy) recipient email addresses.
    /// </summary>
    public Collection<string> Bcc { get; } = bcc ?? new Collection<string>();

    /// <summary>
    /// Gets the collection of CC (Carbon Copy) recipient email addresses.
    /// </summary>
    public Collection<string> Cc { get; } = cc ?? new Collection<string>();

    /// <summary>
    /// Gets the dictionary of attachment data, where the key is the attachment name and the value is the byte array of the attachment content.
    /// </summary>
    public IDictionary<string, byte[]> AttachmentData { get; } = attachmentData ?? new Dictionary<string, byte[]>();

    /// <summary>
    /// Gets the dictionary of custom email headers, where the key is the header name and the value is the header content.
    /// </summary>
    public IDictionary<string, string> Headers { get; } = headers ?? new Dictionary<string, string>();
}
