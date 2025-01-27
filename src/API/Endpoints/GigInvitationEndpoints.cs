using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.GigInvitations.GetGigInvitationById;
using Platform.Application.Features.GigInvitations.GetPublicGigOpportunityById;
using Platform.Application.Features.GigInvitations.GetUserGigInvitation;
using Platform.Application.Features.GigInvitations.UpdateGigInvitationStatus;
using Platform.Application.Features.GigOpportunities.GetGigOpportunityById;
using Platform.Application.Features.MemberRequests.SearchMemberRequests;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the gig invitation endpoints.
/// </summary>
public class GigInvitationEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the gig invitation endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/gig-opportunities/invitations").WithTags("GigInvitation").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] GetUserGigInvitationRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get user gig invitations");

        group
            .MapGet(
                "/{gigInvitationId}",
                async (IMediator mediator, GigApplicantInvitationId gigInvitationId) =>
                {
                    var result = await mediator.Send(new GetGigInvitationByIdRequest(gigInvitationId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get a gig invitation by id");

        group
            .MapPatch(
                "/{gigInvitationId}",
                async (IMediator mediator, GigApplicantInvitationId gigInvitationId, UpdateGigInvitationStatusRequest request) =>
                {
                    var result = await mediator.Send(request with { GigInvitationId = gigInvitationId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update a gig invitation status");

        var publicGroup = builder.MapGroup("api/gig-opportunities/public").WithTags("GigInvitation").AllowAnonymous();

        publicGroup
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] GetPublicGigInvitationRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get public gig invitations");

        publicGroup
            .MapGet(
                "/{memberRequestId}",
                async (IMediator mediator, MemberRequestId memberRequestId) =>
                {
                    var result = await mediator.Send(new GetPublicGigOpportunityByIdRequest(memberRequestId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get a public gig invitation by id");
    }
}
