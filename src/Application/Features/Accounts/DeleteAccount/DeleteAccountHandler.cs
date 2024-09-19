using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.DeleteAccount;

/// <summary>
/// Handles the deletion of user accounts.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="userManager">The user manager.</param>
public class DeleteAccountHandler(IContext context, UserManager<ApplicationUser> userManager) : IRequestHandler<DeleteAccountRequest, Result>
{
    /// <summary>
    /// Handles the deletion of user accounts.
    /// </summary>
    /// <param name="request">The request containing the user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(request.UserId);
        if (user == null)
        {
            return Result.NotFound("User not found.");
        }
        if (user.UserName != request.UserName)
        {
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "userName",
                    ErrorMessage = "Invalid user name.",
                    ErrorCode = "invalid_user_name",
                }
            );
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
        await userManager.DeleteAsync(user);
        return Result.Success();
    }
}
