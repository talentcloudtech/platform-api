using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Azure.Communication.Email;
using Microsoft.Extensions.Options;
using Platform.Api.Configurations;
using Platform.Api.Configurations.OptionSetups;
using Platform.Domain.Interfaces;

namespace Platform.Application.Services.Email;

/// <summary>
/// Implements the IEmailService interface using SMTP for sending emails.
/// </summary>
public class AzureEmailService : IEmailService
{
    private readonly EmailConfigurationOptions _mailOptions;
    private readonly EmailClient _emailClient;

    /// <summary>
    /// Initializes a new instance of the AzureEmailService class.
    /// </summary>
    /// <param name="mailOptions">The mail options containing SMTP configuration.</param>
    public AzureEmailService(IOptions<EmailConfigurationOptions> mailOptions)
    {
        _mailOptions = mailOptions.Value;
        string? connectionString = EnvironmentVariables.CommunicationServicesConnectionString;
        if (connectionString != null)
        {
            _emailClient = new EmailClient(connectionString);
        }
        else
        {
            throw new InvalidOperationException("COMMUNICATION_SERVICES_CONNECTION_STRING is not set.");
        }
    }

    /// <summary>
    /// Sends an email asynchronously using SMTP.
    /// </summary>
    /// <param name="mailRequest">The mail request containing the email details.</param>
    public async Task SendEmailAsync(MailRequest mailRequest)
    {
        var emailMessage = CreateEmailMessage(mailRequest);
        await _emailClient.SendAsync(Azure.WaitUntil.Started, emailMessage);
    }

    /// <summary>
    /// Sends multiple emails asynchronously using SMTP.
    /// </summary>
    /// <param name="mailRequests">The mail requests containing the email details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendEmailAsync(List<MailRequest> mailRequests)
    {
        var emailMessages = mailRequests.Select(CreateEmailMessage).ToArray();
        foreach (var emailMessage in emailMessages)
        {
            await _emailClient.SendAsync(Azure.WaitUntil.Started, emailMessage);
        }
    }

    /// <summary>
    /// Sends a templated email asynchronously using SMTP.
    /// </summary>
    /// <param name="templateName">The name of the email template to use.</param>
    /// <param name="mailRequest">The mail request containing the email details.</param>
    /// <param name="model">The model containing data to populate the template.</param>
    public async Task SendTemplatedEmailAsync(string templateName, MailRequest mailRequest, object model)
    {
        // TODO: Implement template rendering logic here
        // For now, we'll just send a regular email
        await SendEmailAsync(mailRequest);
    }

    /// <summary>
    /// Creates a EmailMessage object from the MailRequest.
    /// </summary>
    /// <param name="mailRequest">The mail request containing the email details.</param>
    /// <returns>A EmailMessage object.</returns>
    private EmailMessage CreateEmailMessage(MailRequest mailRequest)
    {
        var content = new EmailContent(mailRequest.Subject);
        var toRecipients = mailRequest.To.Select(to => new EmailAddress(to)).ToList();
        var bccRecipients = mailRequest.Bcc.Select(bcc => new EmailAddress(bcc)).ToList();
        var ccRecipients = mailRequest.Cc.Select(cc => new EmailAddress(cc)).ToList();

        var emailRecipients = new EmailRecipients(toRecipients, bccRecipients, ccRecipients);

        string FilePath = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Templates", "DefaultTemplate.html"));
        using StreamReader str = new(FilePath);
        string htmlBody = str.ReadToEnd();

        htmlBody = htmlBody.Replace("[content]", mailRequest.Body);

        var emailContent = new EmailContent(mailRequest.Subject) { PlainText = mailRequest.Body, Html = htmlBody };

        var emailMessage = new EmailMessage(senderAddress: _mailOptions.From, emailRecipients, emailContent);

        if (mailRequest.ReplyTo != null)
        {
            emailMessage.ReplyTo.Add(new EmailAddress(mailRequest.ReplyTo, mailRequest.ReplyToName));
        }

        // add attachments if exists
        foreach (var attachment in mailRequest.AttachmentData)
        {
            emailMessage.Attachments.Add(new EmailAttachment(attachment.Key, MediaTypeNames.Application.Octet, new BinaryData(attachment.Value)));
        }

        if (mailRequest.Headers != null)
        {
            foreach (var header in mailRequest.Headers)
            {
                emailMessage.Headers.Add(header.Key, header.Value);
            }
        }
        return emailMessage;
    }
}
