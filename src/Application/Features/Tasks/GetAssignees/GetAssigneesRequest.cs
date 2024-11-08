using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using System.Collections.Generic;

namespace Platform.Application.Features.Tasks.GetAssignees;

/// <summary>
/// Request to get assignees for a task
/// </summary>
/// <param name="WorkspaceId">The workspace ID.</param>
/// <param name="ProjectId">The project ID.</param>
/// <param name="TaskId">The task ID.</param>
public record GetAssigneesRequest(WorkspaceId WorkspaceId, ProjectId ProjectId, ProjectTaskId TaskId)
    : IRequest<Result<List<AssigneeResponse>>>;