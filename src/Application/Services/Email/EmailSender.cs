using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Platform.Api.Configurations;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Interfaces;

namespace Platform.Application.Services.Email;

/// <summary>
/// Implements the IEmailSender interface for sending emails to ApplicationUsers.
/// </summary>
public class EmailSender : IEmailSender<ApplicationUser>, IEmailSender
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
        // create a link to the reset password page with the reset code as a parameter
        var resetLink = $"{EnvironmentVariables.FrontendUrl}/reset-password?code={resetCode}";
        var body =
            $"Please reset your password by <a href='{resetLink}'>clicking here</a>. If link does not work, copy and paste the following code into the password reset form: <b>{resetCode}</b>.<br/>If you did not request a password reset, please ignore this email.";
        var mailRequest = new MailRequest(to: [email], subject: "Your password reset code", body: body);
        await _emailService.SendEmailAsync(mailRequest);
    }

    /// <summary>
    /// Sends a password reset code to the specified email address.
    /// </summary>
    /// <param name="workspaceName">The workspace name.</param>
    /// <param name="invitations"> The invitations to send to the users. </param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task SendWorkspaceInvitationAsync(string workspaceName, List<WorkspaceMemberInvitation> invitations)
    {
        var mailRequests = new List<MailRequest>();
        foreach (var invitation in invitations)
        {
            var invitationLink = $"{EnvironmentVariables.FrontendUrl}/workspaces/invitations/{invitation.Id}";
            var signupLink = $"{EnvironmentVariables.FrontendUrl}/signup";
            var body =
                $@"
                You've been invited to join {workspaceName} by the workspace admin. This workspace provides a platform for seamless collaboration and efficient teamwork.
                <br>
                If you already have an account with us, simply click the button below to accept the invitation and start working with the team:
                <div style='text-align: center;'>
                    <a href='{invitationLink}' style='background-color: green; color: white; padding: 10px 20px; text-decoration: none; display: inline-block;'>Accept Invitation</a>
                </div>
                <br>
                If you don't have an account yet, don't worry! Click the link below to sign up and join the workspace:
                <a href='{signupLink}'>Sign Up and Join Workspace</a>
                <br>
                We look forward to seeing you on board.
                <br>
            ";

            mailRequests.Add(new MailRequest(to: [invitation.Email], subject: $"You've Been Invited to Join {workspaceName}!", body: body));
        }
        await _emailService.SendEmailAsync(mailRequests);
    }
}
