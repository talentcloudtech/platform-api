using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Workspaces.CreateWorkspace;
using Platform.Application.Features.Workspaces.DeleteWorkspace;
using Platform.Application.Features.Workspaces.GetUserWorkspaceRole;
using Platform.Application.Features.Workspaces.GetWorkspaceById;
using Platform.Application.Features.Workspaces.SearchWorkspaces;
using Platform.Application.Features.Workspaces.UpdateWorkspace;
using Platform.Application.Features.Workspaces.UpdateWorkspaceCreditBalance;
using Platform.Application.Features.Workspaces.UploadWorkspaceAvatar;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Endpoints for the Workspace entity.
/// </summary>
public class WorkspaceEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the workspace endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces").WithTags("Workspace").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchWorkspacesRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for workspaces.");

        group
            .MapGet(
                "{workSpaceId}",
                async (IMediator mediator, WorkspaceId workspaceId) =>
                {
                    var result = await mediator.Send(new GetWorkspaceByIdRequest(workspaceId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a workspace by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession session, CreateWorkspaceRequest request) =>
                {
                    var result = await mediator.Send(request with { Role = session.Role!, UserId = session.UserId! });

                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new workspace.");

        group
            .MapPatch(
                "{workSpaceId}",
                async (IMediator mediator, WorkspaceId workspaceId, UpdateWorkspaceRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a workspace by its unique identifier.");

        group
            .MapDelete(
                "{workSpaceId}",
                async (IMediator mediator, WorkspaceId workspaceId) =>
                {
                    var result = await mediator.Send(new DeleteWorkspaceRequest(workspaceId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a workspace by its unique identifier.");

        group
            .MapPost(
                "/{workSpaceId}/logo",
                async (IMediator mediator, ICurrentSession currentSession, WorkspaceId workspaceId, IFormFile file) =>
                {
                    var result = await mediator.Send(
                        new UploadWorkspaceAvatarRequest
                        {
                            CurrentUserId = currentSession.UserId,
                            WorkspaceId = workspaceId,
                            File = file,
                        }
                    );
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update workspace logo.")
            .DisableAntiforgery();

        group
            .MapGet(
                "{workSpaceId}/role",
                async (IMediator mediator, WorkspaceId workspaceId) =>
                {
                    var result = await mediator.Send(new GetUserWorkspaceRoleRequest(workspaceId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get the role of the current user in the workspace.");

        group
            .MapPost(
                "{workSpaceId}/credit-balance",
                async (IMediator mediator, WorkspaceId workspaceId, UpdateWorkspaceCreditBalanceRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update workspace credit balance.");
    }
}
