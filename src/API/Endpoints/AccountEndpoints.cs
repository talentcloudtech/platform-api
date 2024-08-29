using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Features.Accounts.UpdateAvatar;

namespace Platform.Api.Endpoints;

/// <summary>
/// Represents the endpoints for account.
/// </summary>
public static class AccountEndpoints
{
    /// <summary>
    /// Maps the endpoints for account.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/account").WithTags("Account").RequireAuthorization();

        group
            .MapPut(
                "/avatar",
                async (IMediator mediator, UpdateAvatarRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Update avatar.");
    }
}
