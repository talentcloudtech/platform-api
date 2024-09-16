using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Application.Features.Accounts.Login;
using Platform.Domain.Entities;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.Accounts.RefreshToken;

/// <summary>
/// Handler for the RefreshTokenRequest.
/// </summary>
public class RefreshTokenHandler(IContext context, ITokenService tokenService, IOptions<JwtSettingsOptions> jwtSettings)
    : IRequestHandler<RefreshTokenRequest, Result<LoginResponse>>
{
    /// <summary>
    /// Handles the RefreshTokenRequest.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result indicating the success or failure of the operation.</returns>
    public async Task<Result<LoginResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var existingRefreshToken = await context
            .RefreshTokens.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);
        if (existingRefreshToken == null || !existingRefreshToken.IsActive || existingRefreshToken.User == null)
        {
            return Result.Unauthorized();
        }

        existingRefreshToken.Revoked = DateTime.UtcNow;

        var newRefreshToken = await tokenService.CreateRefreshTokenAsync(existingRefreshToken.User);
        var newAccessToken = await tokenService.CreateAccessTokenAsync(existingRefreshToken.User);

        var authResponse = new LoginResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresIn = jwtSettings.Value.AccessTokenExpirationMinutes,
        };

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(authResponse);
    }
}
