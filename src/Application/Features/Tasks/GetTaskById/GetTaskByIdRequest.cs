using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Tasks.GetTaskById;

/// <summary>
/// Represents a request to get a task by its ID.
/// </summary>
/// <param name="WorkspaceId">The workspace ID.</param>
/// <param name="ProjectId">The project ID.</param>
/// <param name="Id">The task ID.</param>
public record GetTaskByIdRequest(WorkspaceId WorkspaceId, ProjectId ProjectId, ProjectTaskId Id) : IRequest<Result<TaskResponse>>;
