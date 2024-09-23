using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;
using Platform.Application.Features.WorkspaceMembers.DeleteWorkspaceMember;
using Platform.Application.Features.WorkspaceMembers.GetWorkspaceMemberById;
using Platform.Application.Features.WorkspaceMembers.SearchWorkspaceMembers;
using Platform.Application.Features.WorkspaceMembers.UpdateWorkspaceMember;
using Platform.Domain.Auth.Interfaces;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the workspace member endpoints.
/// </summary>
public static class WorkspaceMemberEndpoints
{
    /// <summary>
    /// Maps the workspace member endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapWorkspaceMemberEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/members").WithTags("WorkspaceMember").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, string workspaceId, [AsParameters] SearchWorkspaceMembersRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return Results.Ok(result);
                }
            )
            .WithSummary("Searches for workspace members.");

        group
            .MapGet(
                "{userId}",
                async (IMediator mediator, string workspaceId, string userId) =>
                {
                    var result = await mediator.Send(new GetWorkspaceMemberByIdRequest(workspaceId, userId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a workspace member by their unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, string workspaceId, CreateWorkspaceMemberRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new workspace member.");

        group
            .MapPut(
                "{userId}",
                async (IMediator mediator, ICurrentSession currentSession, string workspaceId, string userId, UpdateWorkspaceMemberRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, UserId = userId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a workspace member.");

        group
            .MapDelete(
                "{userId}",
                async (IMediator mediator, ICurrentSession currentSession, string workspaceId, string userId) =>
                {
                    var result = await mediator.Send(new DeleteWorkspaceMemberRequest(workspaceId, userId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a workspace member.");
    }
}
