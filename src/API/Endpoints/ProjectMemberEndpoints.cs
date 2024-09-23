using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.ProjectMembers.CreateProjectMember;
using Platform.Application.Features.ProjectMembers.DeleteProjectMember;
using Platform.Application.Features.ProjectMembers.GetProjectMemberById;
using Platform.Application.Features.ProjectMembers.SearchProjectMembers;
using Platform.Application.Features.ProjectMembers.UpdateProjectMember;
using Platform.Domain.Entities.Common;

namespace Platform.Api.Endpoints;

/// <summary>
/// Endpoints for project members.
/// </summary>
public static class ProjectMemberEndpoints
{
    /// <summary>
    /// Maps the project member endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapProjectMemberEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/projects/{projectId}/members").WithTags("ProjectMember").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, [AsParameters] SearchProjectMembersRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for project members.");

        group
            .MapGet(
                "{userId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, UserId userId) =>
                {
                    var result = await mediator.Send(new GetProjectMemberByIdRequest(workspaceId, projectId, userId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a project member by their unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, CreateProjectMemberRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new project member.");

        group
            .MapPut(
                "{userId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, UserId userId, UpdateProjectMemberRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId, UserId = userId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a project member.");

        group
            .MapDelete(
                "{userId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, UserId userId) =>
                {
                    var result = await mediator.Send(new DeleteProjectMemberRequest(workspaceId, projectId, userId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a project member.");
    }
}
