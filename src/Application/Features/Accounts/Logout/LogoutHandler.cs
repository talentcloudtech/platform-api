using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Accounts.Logout;

/// <summary>
/// Handler for logging out a user.
/// </summary>
public class LogoutHandler(IContext context) : IRequestHandler<LogoutRequest, Result>
{
    /// <summary>
    /// Handles the logout request.
    /// </summary>
    /// <param name="request">The logout request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the logout request.</returns>
    public async Task<Result> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        if (request.LogoutFromAllDevices)
        {
            await context
                .RefreshTokens.Where(x => x.UserId == request.UserId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.Revoked, DateTime.UtcNow), cancellationToken);
        }
        else
        {
            var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);
            if (refreshToken == null)
            {
                return Result.Success();
            }

            refreshToken.Revoked = DateTime.UtcNow;

            await context.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
