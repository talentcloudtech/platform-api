using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ChangePassword;

/// <summary>
/// Handles the change password request.
/// </summary>
public class ChangePasswordHandler(UserManager<ApplicationUser> userManager, ICurrentSession session) : IRequestHandler<ChangePasswordRequest, Result>
{
    /// <summary>
    /// Handles the change password request.
    /// </summary>
    /// <param name="request">The change password request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the change password request.</returns>
    public async Task<Result> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(session.UserId.ToString());
        if (user == null)
            return Result.NotFound("User not found.");

        var passwordCheck = await userManager.CheckPasswordAsync(user, request.CurrentPassword);
        if (!passwordCheck)
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "currentPassword",
                    ErrorMessage = "Current password is incorrect.",
                    ErrorCode = "invalid_password",
                }
            );

        var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
            return Result.Invalid(
                result.Errors.Select(e => new ValidationError
                {
                    Identifier = "newPassword",
                    ErrorMessage = e.Description,
                    ErrorCode = e.Code.ToLower(),
                })
            );
        return Result.Success();
    }
}
