using System.Threading.Tasks;
using Platform.Application.Services.Email;

namespace Platform.Domain.Interfaces;

/// <summary>
/// Defines the contract for an email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="mailRequest">The mail request containing the email details.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendEmailAsync(MailRequest mailRequest);

    /// <summary>
    /// Sends a templated email asynchronously.
    /// </summary>
    /// <param name="templateName">The name of the email template to use.</param>
    /// <param name="mailRequest">The mail request containing the email details.</param>
    /// <param name="model">The model containing data to populate the template.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SendTemplatedEmailAsync(string templateName, MailRequest mailRequest, object model);
}
