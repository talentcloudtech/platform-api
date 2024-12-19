using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Extensions;
using Platform.Application.Features.Tasks.CreateTask;
using Platform.Application.Features.Tasks.DeleteTask;
using Platform.Application.Features.Tasks.GetAssignees;
using Platform.Application.Features.Tasks.GetTaskById;
using Platform.Application.Features.Tasks.PublishRejectTask;
using Platform.Application.Features.Tasks.SearchOpenTasks;
using Platform.Application.Features.Tasks.SearchTasks;
using Platform.Application.Features.Tasks.SearchUsersTask;
using Platform.Application.Features.Tasks.UpdateTask;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Endpoints for project tasks.
/// </summary>
public class TaskEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the project task endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
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

        builder
            .MapGet(
                "api/tasks/open",
                async (IMediator mediator, [AsParameters] SearchOpenTasksRequest request) =>
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
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, [AsParameters] SearchTasksRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for tasks.");

        group
            .MapGet(
                "{taskId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId) =>
                {
                    var result = await mediator.Send(new GetTaskByIdRequest(workspaceId, projectId, taskId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a task by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, CreateTaskRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new task.");

        group
            .MapPatch(
                "{taskId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId, UpdateTaskRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId, Id = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a task.");

        group
            .MapDelete(
                "{taskId}",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId) =>
                {
                    var result = await mediator.Send(new DeleteTaskRequest(workspaceId, projectId, taskId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a task.");

        group
            .MapPost(
                "{taskId}/assign",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId, CreateTaskAssigneeRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId, TaskId = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Adds a task assignee.");

        group
            .MapPatch(
                "{taskId}/assign",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId, UpdateTaskAssigneeRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, ProjectId = projectId, TaskId = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a task assignee.");

        group
            .MapGet(
                "{taskId}/assignees",
                async (IMediator mediator, ProjectId projectId, ProjectTaskId taskId) =>
                {
                    var request = new GetTaskAssigneesRequest(projectId, taskId);
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get all assignees for a task");

        group
            .MapPost(
                "{taskId}/publish-reject",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId projectId, ProjectTaskId taskId) =>
                {
                    var request = new PublishRejectTaskRequest
                    {
                        WorkspaceId = workspaceId,
                        ProjectId = projectId,
                        TaskId = taskId,
                    };
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Publish a task");
    }
}
