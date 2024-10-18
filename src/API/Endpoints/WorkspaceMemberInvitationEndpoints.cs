using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.WorkspaceMemberInvitations.DeleteWorkspaceMemberInvitation;
using Platform.Application.Features.WorkspaceMemberInvitations.GetWorkspaceMemberInvitationById;
using Platform.Application.Features.WorkspaceMemberInvitations.ResendWorkspaceMemberInvitation;
using Platform.Application.Features.WorkspaceMemberInvitations.SearchWorkspaceMemberInvitations;
using Platform.Application.Features.WorkspaceMemberInvitations.UpdateWorkspaceMemberInvitation;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the workspace member invitation endpoints.
/// </summary>
public class WorkspaceMemberInvitationEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the workspace member invitation endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces").WithTags("WorkspaceMemberInvitation").RequireAuthorization();

        group
            .MapGet(
                "/{workspaceId}/invitations",
                async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceMemberInvitationsRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
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

        group
            .MapPost(
                "invitations/{invitationId}/resend",
                async (IMediator mediator, WorkspaceMemberInvitationId invitationId) =>
                {
                    var result = await mediator.Send(new ResendWorkspaceMemberInvitationRequest { InvitationId = invitationId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Resend a workspace member invitation mail.");

        group
            .MapDelete(
                "invitations/{invitationId}",
                async (IMediator mediator, WorkspaceMemberInvitationId invitationId) =>
                {
                    var result = await mediator.Send(new DeleteWorkspaceMemberInvitationRequest(invitationId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a workspace member invitation.");
    }
}
