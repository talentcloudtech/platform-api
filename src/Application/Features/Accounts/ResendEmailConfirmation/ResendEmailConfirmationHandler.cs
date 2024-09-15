using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Api.Configurations;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ResendEmailConfirmation;

/// <summary>
/// Handler for resending an email confirmation.
/// </summary>
public class ResendEmailConfirmationHandler(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser> emailSender)
    : IRequestHandler<ResendEmailConfirmationRequest, Result>
{
    /// <summary>
    /// Handles the request to resend an email confirmation.
    /// </summary>
    /// <param name="request">The request to resend an email confirmation.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(ResendEmailConfirmationRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.NotFound();
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"{EnvironmentVariables.BackendUrl}/api/accounts/confirmEmail?userId={user.Id}&code={token}";
        await emailSender.SendConfirmationLinkAsync(user, user.Email!, confirmationLink);

        return Result.Success();
    }
}
