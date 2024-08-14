using Ardalis.Result.AspNetCore;
using MediatR;
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
        var group = builder.MapGroup("api/projects").WithTags("Project").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchProjectsRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result;
                }
            )
            .WithSummary("Searches for projects.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, ProjectId id) =>
                {
                    var result = await mediator.Send(new GetProjectByIdRequest(id));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a project by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, CreateProjectRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new project.");

        group
            .MapPut(
                "{id}",
                async (IMediator mediator, ProjectId id, UpdateProjectRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = id });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates a project by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, ProjectId id) =>
                {
                    var result = await mediator.Send(new DeleteProjectRequest(id));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes a project by its unique identifier.");
    }
}
