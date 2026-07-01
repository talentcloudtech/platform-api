using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.API.Configurations;
using Platform.Application.Extensions;
using Platform.Application.Features.Accounts.ChangeEmail;
using Platform.Application.Features.Accounts.ChangePassword;
using Platform.Application.Features.Accounts.ConfirmEmail;
using Platform.Application.Features.Accounts.DeleteAccount;
using Platform.Application.Features.Accounts.ForgotPassword;
using Platform.Application.Features.Accounts.GetAccountDetails;
using Platform.Application.Features.Accounts.Login;
using Platform.Application.Features.Accounts.Logout;
using Platform.Application.Features.Accounts.RefreshToken;
using Platform.Application.Features.Accounts.RegisterAccount;
using Platform.Application.Features.Accounts.ResendEmailConfirmation;
using Platform.Application.Features.Accounts.ResetPassword;
using Platform.Application.Features.Accounts.UpdateUserFullName;
using Platform.Application.Features.Accounts.UpdateUserName;
using Platform.Domain.Auth.Interfaces;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for account-related operations.
/// </summary>
public class AccountEndpoints : IEndpoints
{
    private const string RefreshTokenCookieName = "refreshToken";

    private static void SetRefreshTokenCookie(HttpContext httpContext, string refreshToken)
    {
        httpContext.Response.Cookies.Append(
            RefreshTokenCookieName,
            refreshToken,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/api/accounts",
            }
        );
    }

    private static void ClearRefreshTokenCookie(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete(RefreshTokenCookieName, new CookieOptions { Path = "/api/accounts" });
    }

    /// <summary>
    /// Maps the endpoints for account-related operations.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/accounts").WithTags("Account");

        group
            .MapPost(
                "/login",
                async (IMediator mediator, HttpContext httpContext, LoginRequest request) =>
                {
                    var result = await mediator.Send(request);
                    if (result.IsSuccess)
                    {
                        SetRefreshTokenCookie(httpContext, result.Value.RefreshToken);
                    }
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Login user.")
            .AllowAnonymous();

        group
            .MapGet(
                "/confirmEmail",
                async (IMediator mediator, [AsParameters] ConfirmEmailRequest request) =>
                {
                    var result = await mediator.Send(request);
                    if (result.IsSuccess)
                    {
                        return Results.Redirect(EnvironmentVariables.FrontendUrl);
                    }
                    else
                    {
                        return Results.UnprocessableEntity(result.Errors.Select(x => x).ToArray());
                    }
                }
            )
            .WithSummary("Confirm email.")
            .AllowAnonymous();

        group
            .MapPost(
                "/resendConfirmationEmail",
                async (IMediator mediator, ResendEmailConfirmationRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Resend email confirmation.")
            .AllowAnonymous();

        // register account
        group
            .MapPost(
                "/register",
                async (IMediator mediator, RegisterAccountRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Register account.")
            .AllowAnonymous();

        group
            .MapPost(
                "/forgotPassword",
                async (IMediator mediator, ForgotPasswordRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Forgot password.")
            .AllowAnonymous();

        group
            .MapPost(
                "/resetPassword",
                async (IMediator mediator, ResetPasswordRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Reset password.")
            .AllowAnonymous();

        group
            .MapPost(
                "/refresh",
                async (IMediator mediator, HttpContext httpContext) =>
                {
                    if (!httpContext.Request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshToken) || string.IsNullOrEmpty(refreshToken))
                    {
                        return Results.Unauthorized();
                    }

                    var result = await mediator.Send(new RefreshTokenRequest(refreshToken));
                    if (result.IsSuccess)
                    {
                        SetRefreshTokenCookie(httpContext, result.Value.RefreshToken);
                    }
                    else
                    {
                        ClearRefreshTokenCookie(httpContext);
                    }
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Refresh token.")
            .AllowAnonymous();

        group
            .MapPost(
                "/logout",
                async (IMediator mediator, HttpContext httpContext, ICurrentSession currentUser, LogoutRequest request) =>
                {
                    httpContext.Request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshToken);
                    var result = await mediator.Send(
                        request with
                        {
                            UserId = currentUser.UserId,
                            RefreshToken = refreshToken ?? request.RefreshToken,
                        }
                    );
                    ClearRefreshTokenCookie(httpContext);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Logout user.")
            .RequireAuthorization();

        group
            .MapPut(
                "/delete",
                async (IMediator mediator, ICurrentSession currentUser, DeleteAccountRequest request) =>
                {
                    var result = await mediator.Send(request with { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Delete user account from the database.")
            .RequireAuthorization();

        group
            .MapGet(
                "/me",
                async (IMediator mediator, ICurrentSession currentUser) =>
                {
                    var result = await mediator.Send(new GetAccountDetailsRequest { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get current user account details.")
            .RequireAuthorization();

        group
            .MapPut(
                "/username",
                async (IMediator mediator, ICurrentSession currentUser, UpdateUsernameRequest request) =>
                {
                    var result = await mediator.Send(request with { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update username.")
            .RequireAuthorization();

        group
            .MapPut(
                "/display-name",
                async (IMediator mediator, ICurrentSession currentUser, UpdateUserFullNameRequest request) =>
                {
                    var result = await mediator.Send(request with { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update username.")
            .RequireAuthorization();

        group
            .MapPut(
                "/change-email",
                async (IMediator mediator, ICurrentSession currentUser, ChangeEmailRequest request) =>
                {
                    var result = await mediator.Send(request with { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Change email.")
            .RequireAuthorization();

        group
            .MapPut(
                "/change-password",
                async (IMediator mediator, ICurrentSession currentUser, ChangePasswordRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Change password.")
            .RequireAuthorization();
    }
}
