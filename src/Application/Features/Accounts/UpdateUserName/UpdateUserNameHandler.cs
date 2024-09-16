using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Accounts.UpdateUserName;

/// <summary>
/// Handler for updating the username of a user.
/// </summary>
/// <param name="userManager">The user manager.</param>
public class UpdateUserNameHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<UpdateUsernameRequest, Result<string>>
{
    /// <summary>
    /// Updates the username of the user.
    /// </summary>
    /// <param name="request">The request containing the new username and user id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<string>> Handle(UpdateUsernameRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            return Result.NotFound($"User with id {request.UserId} not found.");
        }

        user.UserName = request.UserName;
        await userManager.SetUserNameAsync(user, request.UserName);
        return Result.Success();
    }
}
