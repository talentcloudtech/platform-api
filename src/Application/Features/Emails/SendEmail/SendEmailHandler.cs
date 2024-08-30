using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure;
using Azure.Communication.Email;
using MediatR;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;

namespace Platform.Application.Features.Emails.SendEmail;

/// <summary>
/// Handles sending emails using Azure Communication Services.
/// </summary>
public class SendEmailHandler : IRequestHandler<SendEmailRequest, Result>
{
    private readonly EmailClient _emailClient;
    private readonly AzureEmailOptions _azureEmailOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendEmailHandler"/> class.
    /// </summary>
    /// <param name="azureEmailOptions">The options for Azure email configuration.</param>
    public SendEmailHandler(IOptions<AzureEmailOptions> azureEmailOptions)
    {
        _azureEmailOptions = azureEmailOptions.Value;
        _emailClient = new EmailClient(_azureEmailOptions.ConnectionString);
    }

    /// <summary>
    /// Handles the sending of an email.
    /// </summary>
    /// <param name="request">The send email request containing recipient, subject, and content.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure of the email sending operation.</returns>
    public async Task<Result> Handle(SendEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var emailContent = new EmailContent(request.Subject) { Html = request.HtmlContent };

            var emailMessage = new EmailMessage(_azureEmailOptions.SenderAddress, request.To, emailContent);

            var sendOperation = await _emailClient.SendAsync(WaitUntil.Started, emailMessage, cancellationToken).ConfigureAwait(false);

            await sendOperation.WaitForCompletionAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error($"Failed to send email: {ex.Message}");
        }
    }
}
