using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ConfirmEmail;

/// <summary>
/// Handles the confirmation of an email address.
/// </summary>
public class ConfirmEmailHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ConfirmEmailRequest, Result>
{
    /// <summary>
    /// Handles the confirmation of an email address.
    /// </summary>
    /// <param name="request">The request to confirm an email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the confirmation process.</returns>
    public async Task<Result> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result.Error("User not found");
        }

        var result = await userManager.ConfirmEmailAsync(user, request.Code);
        if (!result.Succeeded)
        {
            return Result.Invalid(new ValidationError("Invalid confirmation code."));
        }

        return Result.Success();
    }
}
