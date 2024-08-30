using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;
using Platform.Domain.Interfaces;

namespace Platform.Application.Services.Email;

/// <summary>
/// Implements the IEmailSender interface for sending emails to ApplicationUsers.
/// </summary>
public class EmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the EmailSender class.
    /// </summary>
    /// <param name="emailService">The email service used to send emails.</param>
    public EmailSender(IEmailService emailService)
    {
        _emailService = emailService;
    }

    /// <summary>
    /// Sends a confirmation link to the specified email address.
    /// </summary>
    /// <param name="user">The user requesting email confirmation.</param>
    /// <param name="email">The email address to send the confirmation link to.</param>
    /// <param name="confirmationLink">The confirmation link to be included in the email.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        var mailRequest = new MailRequest(
            to: [email],
            subject: "Confirm your email",
            body: $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>."
        );

        await _emailService.SendEmailAsync(mailRequest);
    }

    /// <summary>
    /// Sends a password reset link to the specified email address.
    /// </summary>
    /// <param name="user">The user requesting password reset.</param>
    /// <param name="email">The email address to send the reset link to.</param>
    /// <param name="resetLink">The password reset link to be included in the email.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        var mailRequest = new MailRequest(
            to: [email],
            subject: "Reset your password",
            body: $"Please reset your password by <a href='{resetLink}'>clicking here</a>."
        );

        await _emailService.SendEmailAsync(mailRequest);
    }

    /// <summary>
    /// Sends a password reset code to the specified email address.
    /// </summary>
    /// <param name="user">The user requesting password reset.</param>
    /// <param name="email">The email address to send the reset code to.</param>
    /// <param name="resetCode">The password reset code to be included in the email.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var mailRequest = new MailRequest(to: [email], subject: "Your password reset code", body: $"Your password reset code is: {resetCode}");

        await _emailService.SendEmailAsync(mailRequest);
    }
}
