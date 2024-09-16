using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ForgotPassword;

/// <summary>
/// Handler for the ForgotPasswordRequest.
/// </summary>
public class ForgotPasswordHandler(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser> emailSender)
    : IRequestHandler<ForgotPasswordRequest, Result>
{
    /// <summary>
    /// Handles the ForgotPasswordRequest.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.Success();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        await emailSender.SendPasswordResetCodeAsync(user, user.Email!, token);

        return Result.Success();
    }
}
