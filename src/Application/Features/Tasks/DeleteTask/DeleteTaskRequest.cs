using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Tasks.DeleteTask;

/// <summary>
/// Represents a request to delete a task.
/// </summary>
/// <param name="WorkspaceId">The workspace ID.</param>
/// <param name="ProjectId">The project ID.</param>
/// <param name="Id">The task ID.</param>
public record DeleteTaskRequest(WorkspaceId WorkspaceId, ProjectId ProjectId, ProjectTaskId Id) : IRequest<Result>;
