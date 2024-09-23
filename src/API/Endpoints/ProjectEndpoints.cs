using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Projects.CreateProject;
using Platform.Application.Features.Projects.DeleteProject;
using Platform.Application.Features.Projects.GetProjectById;
using Platform.Application.Features.Projects.SearchProjects;
using Platform.Application.Features.Projects.UpdateProject;

namespace Platform.Api.Endpoints;

/// <summary>
/// Represents the endpoints for projects.
/// </summary>
public static class ProjectEndpoints
{
    /// <summary>
    /// Maps the endpoints for projects.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapProjectEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/projects").WithTags("Project").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, string workSpaceId, [AsParameters] SearchProjectsRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for projects.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, string workSpaceId, string id) =>
                {
                    var result = await mediator.Send(new GetProjectByIdRequest(workSpaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a project by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, string workSpaceId, CreateProjectRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new project.");

        group
            .MapPatch(
                "{id}",
                async (IMediator mediator, string workSpaceId, string id, UpdateProjectRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId, Id = id });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a project by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, string workspaceId, string id) =>
                {
                    var result = await mediator.Send(new DeleteProjectRequest(workspaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a project by its unique identifier.");
    }
}
