using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Features.Accounts.DeleteAccount;
using Platform.Application.Features.Accounts.GetAccountDetails;
using Platform.Application.Features.Accounts.UpdateAvatar;
using Platform.Application.Features.Accounts.UserRegister;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;

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

        group.MapPost(
            "/register",
            async (IMediator mediator, UserRegisterRequest request) =>
            {
                var result = await mediator.Send(request);
                return result.ToMinimalApiResult();
            }
        );

        group
            .MapPut(
                "/avatar",
                async (IMediator mediator, UpdateAvatarRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToMinimalApiResult();
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
                    return result.ToMinimalApiResult();
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
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Get current user account details.")
            .RequireAuthorization();
    }
}
