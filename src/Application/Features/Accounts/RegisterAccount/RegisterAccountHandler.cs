using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Api.Configurations;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Accounts.RegisterAccount;

/// <summary>
/// Handler for registering a new user account.
/// </summary>
/// <param name="userManager">The user manager to use for creating the user.</param>
/// <param name="emailSender">The email sender to use for sending the confirmation email.</param>
public class RegisterAccountHandler(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser> emailSender)
    : IRequestHandler<RegisterAccountRequest, Result>
{
    /// <summary>
    /// Handles the request to register a new user account.
    /// </summary>
    /// <param name="request">The request to register a new user account.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the registration process.</returns>
    public async Task<Result> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser { UserName = request.UserName.Trim(), Email = request.Email.Trim() };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result.Error(result.Errors.FirstOrDefault()?.Description ?? "Failed to register user");
        }

        // assign role to user
        await userManager.AddToRoleAsync(user, UserRole.User.Name);

        // send email to user
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"{EnvironmentVariables.BackendUrl}/api/accounts/confirmEmail?userId={user.Id}&code={token}";
        await emailSender.SendConfirmationLinkAsync(user, user.Email, confirmationLink);

        return Result.Success();
    }
}
