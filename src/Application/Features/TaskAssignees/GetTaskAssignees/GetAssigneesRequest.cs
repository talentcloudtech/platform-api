using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Tasks.GetAssignees;

/// <summary>
/// Request to get assignees for a task
/// </summary>
/// <param name="ProjectId">The project ID.</param>
/// <param name="TaskId">The task ID.</param>
public record GetTaskAssigneesRequest(ProjectId ProjectId, ProjectTaskId TaskId) : IRequest<Result<List<CommonUserResponse>>>;
