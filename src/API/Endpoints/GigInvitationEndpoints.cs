using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.GigInvitations.GetGigInvitationById;
using Platform.Application.Features.GigInvitations.GetUserGigInvitation;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the gig invitation endpoints.
/// </summary>
public static class GigInvitationEndpoints
{
    /// <summary>
    /// Maps the gig invitation endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapGigInvitationEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/gig-opportunities/{memberRequestId}/invitations").WithTags("GigInvitation").RequireAuthorization();

        group.MapGet(
            "/",
            async (IMediator mediator, ICurrentSession currentUser, MemberRequestId memberRequestId, [AsParameters] GetUserGigInvitationRequest request) =>
            {
                var result = await mediator.Send(request with { MemberRequestId = memberRequestId, UserId = currentUser.UserId });
                return result.ToPlatformApiResult();
            }
        );

        group.MapGet(
            "/{id}",
            async (IMediator mediator, MemberRequestId memberRequestId, GigApplicantInvitationId id) =>
            {
                var result = await mediator.Send(new GetGigInvitationByIdRequest(memberRequestId, id));
                return result.ToPlatformApiResult();
            }
        );
    }
}
