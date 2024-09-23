using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Application.Features.Projects;
using Platform.Application.Features.Projects.CreateProject;
using Platform.Application.Features.Projects.DeleteProject;
using Platform.Application.Features.Projects.GetProjectById;
using Platform.Application.Features.Projects.SearchProjects;
using Platform.Application.Features.Projects.UpdateProject;
using Platform.Domain.Entities.Common;

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
                async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchProjectsRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for projects.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, string id) =>
                {
                    var result = await mediator.Send(new GetProjectByIdRequest(workspaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a project by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, CreateProjectRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new project.");

        group
            .MapPatch(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, string id, UpdateProjectRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, Id = id });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a project by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, string id) =>
                {
                    var result = await mediator.Send(new DeleteProjectRequest(workspaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a project by its unique identifier.");
    }
}
