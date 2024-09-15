using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.ResetPassword;

/// <summary>
/// Handler for the ResetPasswordRequest.
/// </summary>
public class ResetPasswordHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ResetPasswordRequest, Result>
{
    /// <summary>
    /// Handles the ResetPasswordRequest.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result indicating the success or failure of the operation.</returns>
    public async Task<Result> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result.NotFound();
        }

        var result = await userManager.ResetPasswordAsync(user, request.ResetCode, request.NewPassword);
        if (result.Succeeded)
        {
            return Result.Success();
        }

        return Result.Error(new ErrorList(result.Errors.Select(e => e.Description).ToArray()));
    }
}
