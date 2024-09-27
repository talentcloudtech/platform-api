using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.GigOpportunities.CreateGigInvitation;
using Platform.Application.Features.GigOpportunities.GetGigOpportunityById;
using Platform.Application.Features.GigOpportunities.SearchGigOpportunities;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the gig opportunity endpoints.
/// </summary>
public static class GigOpportunityEndpoints
{
    /// <summary>
    /// Maps the gig opportunity endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapGigOpportunityEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/gig-opportunities").WithTags("GigOpportunity").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchGigOpportunitiesRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Search for gig opportunities");

        group
            .MapGet(
                "/{id}",
                async (IMediator mediator, MemberRequestId id) =>
                {
                    var result = await mediator.Send(new GetGigOpportunityByIdRequest(id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get gig opportunity by id");

        group
            .MapPost(
                "/{id}/invitation",
                async (IMediator mediator, MemberRequestId memberRequestId, CreateGigInvitationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberRequestId = memberRequestId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Create a gig invitation");
    }
}
