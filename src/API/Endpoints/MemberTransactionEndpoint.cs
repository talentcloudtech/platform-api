using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.MemberTransactions.CreateMemberTransaction;
using Platform.Application.Features.MemberTransactions.SearchMemberTransaction;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for member requests.
/// </summary>
public class MemberTransactionEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for member requests.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/members/{memberId}/transactions").WithTags("MemberTransaction").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, UserId userId, [AsParameters] SearchMemberTransactionRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberId = userId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for member transactions.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, UserId userId, CreateMemberTransactionRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberId = userId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new member transaction.");
    }
}
