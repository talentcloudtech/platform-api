using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Domain.Entities;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.Accounts.Login;

/// <summary>
/// Handler for logging in a user.
/// </summary>
/// <param name="userManager">The user manager.</param>
/// <param name="signInManager">The sign in manager.</param>
/// <param name="tokenService">The token service.</param>
/// <param name="jwtSettings">The JWT settings.</param>
public class LoginHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService,
    IOptions<JwtSettingsOptions> jwtSettings
) : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly Regex _emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    /// <summary>
    /// Handles the login request.
    /// </summary>
    /// <param name="request">The login request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the login operation.</returns>
    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var query = userManager.Users.Include(u => u.RefreshTokens);

        ApplicationUser? user = _emailRegex.IsMatch(request.UserName)
            ? await query.FirstOrDefaultAsync(u => u.Email == request.UserName)
            : await query.FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (user is null)
        {
            return Result.Invalid(new ValidationError("Invalid credentials."));
        }

        if (!user.EmailConfirmed)
        {
            return Result.Invalid(
                new ValidationError
                {
                    Identifier = "root",
                    ErrorMessage = "Email not confirmed.",
                    ErrorCode = "email_not_confirmed",
                }
            );
        }

        var result = await signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                return Result.Invalid(new ValidationError("User is locked out."));
            }

            return Result.Invalid(new ValidationError("Invalid credentials."));
        }

        var accessToken = await tokenService.CreateAccessTokenAsync(user);
        var refreshToken = await tokenService.CreateRefreshTokenAsync(user);

        var response = new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresIn = jwtSettings.Value.AccessTokenExpirationMinutes,
        };

        return Result.Success(response);
    }
}
