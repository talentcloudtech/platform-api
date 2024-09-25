using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
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
        var group = builder.MapGroup("api/workspaces/{workspaceId}/invitations").WithTags("WorkspaceMemberInvitation").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceMemberInvitationsRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for workspace member invitations.");

        group
            .MapPatch(
                "{invitationId}",
                async (
                    IMediator mediator,
                    ICurrentSession currentSession,
                    WorkspaceId workspaceId,
                    WorkspaceMemberInvitationId invitationId,
                    UpdateWorkspaceMemberInvitationRequest request
                ) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, WorkspaceMemberInvitationId = invitationId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a workspace member invitation (accept or reject).");
    }
}
