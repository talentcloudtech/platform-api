using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ChangeEmail;

/// <summary>
/// Handles the change email request.
/// <param name="userManager">The user manager.</param>
/// </summary>
public class ChangeEmailHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ChangeEmailRequest, Result<string>>
{
    /// <summary>
    /// Handles the change email request.
    /// </summary>
    /// <param name="request">The change email request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the change email request.</returns>
    public async Task<Result<string>> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
        {
            return Result.Error("User not found.");
        }

        var passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordCheck)
        {
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "password",
                    ErrorMessage = "Invalid password.",
                    ErrorCode = "invalid_password",
                }
            );
        }

        var duplicateEmail = await userManager.FindByEmailAsync(request.NewEmail);
        if (duplicateEmail != null)
        {
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "newEmail",
                    ErrorMessage = "Email already exists.",
                    ErrorCode = "email_already_exists",
                }
            );
        }

        var token = await userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
        var result = await userManager.ChangeEmailAsync(user, request.NewEmail, token);
        if (!result.Succeeded)
        {
            return Result.Error(result.Errors.FirstOrDefault()?.Description ?? "Failed to change email");
        }

        return Result.Success("Email changed successfully.");
    }
}
