using Ardalis.Result;
using MediatR;

namespace Platform.Application.Features.Emails.SendEmail;

/// <summary>
/// Represents a request to send an email.
/// </summary>
public class SendEmailRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the email address of the recipient.
    /// </summary>
    public required string To { get; set; }

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// Gets or sets the HTML content of the email body.
    /// </summary>
    public required string HtmlContent { get; set; }
}
