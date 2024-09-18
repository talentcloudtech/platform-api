using System;
using System.Linq;
using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Api.Configurations;
using Platform.Application.Extensions;
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
using Platform.Application.Features.Accounts.UpdateAvatar;
using Platform.Application.Features.Accounts.UpdateUserName;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Api.Endpoints;

/// <summary>
/// Represents the endpoints for account-related operations.
/// </summary>
public static class AccountEndpoints
{
    /// <summary>
    /// Maps the endpoints for account-related operations.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/accounts").WithTags("Account");

        group
            .MapPost(
                "/login",
                async (IMediator mediator, LoginRequest request) =>
                {
                    var result = await mediator.Send(request);
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
                        return Results.BadRequest(result.Errors.Select(x => x).ToArray());
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
                async (IMediator mediator, RefreshTokenRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Refresh token.")
            .AllowAnonymous();

        group
            .MapPost(
                "/logout",
                async (IMediator mediator, ICurrentSession currentUser, LogoutRequest request) =>
                {
                    var result = await mediator.Send(request with { UserId = currentUser.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Logout user.")
            .RequireAuthorization();

        group
            .MapPut(
                "/avatar",
                async (IMediator mediator, UpdateAvatarRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update avatar.")
            .RequireAuthorization();

        group
            .MapDelete(
                "/",
                async (IMediator mediator, ICurrentSession currentUser) =>
                {
                    var result = await mediator.Send(new DeleteAccountRequest() with { UserId = currentUser.UserId });
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
    }
}
