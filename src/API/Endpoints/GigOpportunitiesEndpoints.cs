using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.GigOpportunities.CreateGigInvitation;
using Platform.Application.Features.GigOpportunities.GetGigOpportunityById;
using Platform.Application.Features.GigOpportunities.GetGigOpportunityCandidates;
using Platform.Application.Features.GigOpportunities.SearchGigOpportunities;
using Platform.Application.Features.GigOpportunities.UpdateGigOpportunityCandidate;
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
                "/{memberRequestId}",
                async (IMediator mediator, MemberRequestId memberRequestId) =>
                {
                    var result = await mediator.Send(new GetGigOpportunityByIdRequest(memberRequestId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get gig opportunity by member request id");

        group
            .MapPost(
                "/{memberRequestId}/invitation",
                async (IMediator mediator, MemberRequestId memberRequestId, CreateGigInvitationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberRequestId = memberRequestId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Create a gig invitation for a member request");

        group
            .MapGet(
                "/{memberRequestId}/candidates",
                async (IMediator mediator, MemberRequestId memberRequestId) =>
                {
                    var result = await mediator.Send(new GetGigOpportunityCandidatesRequest(memberRequestId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get gig opportunity candidates by member request id");

        group
            .MapPatch(
                "/{memberRequestId}/candidates/{userId}",
                async (IMediator mediator, MemberRequestId memberRequestId, UserId userId, UpdateGigOpportunityCandidateRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberRequestId = memberRequestId, UserId = userId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update gig opportunity candidate status");
    }
}
