using System;
using System.Linq;
using System.Linq.Expressions;
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
        Expression<Func<Domain.Entities.RefreshToken, bool>> predicate = request.LogoutFromAllDevices
            ? x => x.UserId == request.UserId
            : x => x.Token == request.RefreshToken;

        var refreshToken = await context.RefreshTokens.Where(predicate).ExecuteDeleteAsync(cancellationToken);

        return Result.Success();
    }
}
