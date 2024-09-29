using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.WorkspaceMemberInvitations.GetWorkspaceMemberInvitationById;
using Platform.Application.Features.WorkspaceMemberInvitations.SearchWorkspaceMemberInvitations;
using Platform.Application.Features.WorkspaceMemberInvitations.UpdateWorkspaceMemberInvitation;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the workspace member invitation endpoints.
/// </summary>
public static class WorkspaceMemberInvitationEndpoints
{
    /// <summary>
    /// Maps the workspace member invitation endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapWorkspaceMemberInvitationEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces").WithTags("WorkspaceMemberInvitation").RequireAuthorization();

        group
            .MapGet(
                "/{workspaceId}/invitations",
                async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceMemberInvitationsRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for workspace member invitations.");

        group
            .MapGet(
                "invitations/{invitationId}",
                async (IMediator mediator, WorkspaceMemberInvitationId invitationId) =>
                {
                    var result = await mediator.Send(new GetWorkspaceMemberInvitationByIdRequest(invitationId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Gets a workspace member invitation by ID.");

        group
            .MapPatch(
                "invitations/{invitationId}",
                async (IMediator mediator, WorkspaceMemberInvitationId invitationId, UpdateWorkspaceMemberInvitationRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceMemberInvitationId = invitationId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a workspace member invitation (accept or reject).");
    }
}
