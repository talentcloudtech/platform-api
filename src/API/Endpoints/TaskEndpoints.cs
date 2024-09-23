using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Tasks.CreateTask;
using Platform.Application.Features.Tasks.DeleteTask;
using Platform.Application.Features.Tasks.GetTaskById;
using Platform.Application.Features.Tasks.SearchTasks;
using Platform.Application.Features.Tasks.UpdateTask;

namespace Platform.Api.Endpoints;

/// <summary>
/// Endpoints for project tasks.
/// </summary>
public static class TaskEndpoints
{
    /// <summary>
    /// Maps the project task endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapProjectTaskEndpoints(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet(
                "api/tasks",
                async (IMediator mediator, [AsParameters] SearchUsersTaskRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for user's tasks.")
            .RequireAuthorization();

        var group = builder.MapGroup("api/workspaces/{workspaceId}/projects/{projectId}/tasks").WithTags("Task").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, string workspaceId, string projectId, [AsParameters] SearchTasksRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for tasks.");

        group
            .MapGet(
                "{taskId}",
                async (IMediator mediator, string workspaceId, string projectId, string taskId) =>
                {
                    var result = await mediator.Send(new GetTaskByIdRequest(workspaceId, projectId, taskId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a task by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, string workspaceId, string projectId, CreateTaskRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new task.");

        group
            .MapPatch(
                "{taskId}",
                async (IMediator mediator, string workspaceId, string projectId, string taskId, UpdateTaskRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId, Id = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a task.");

        group
            .MapDelete(
                "{taskId}",
                async (IMediator mediator, string workspaceId, string projectId, string taskId) =>
                {
                    var result = await mediator.Send(new DeleteTaskRequest(workspaceId, projectId, taskId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a task.");
    }
}
