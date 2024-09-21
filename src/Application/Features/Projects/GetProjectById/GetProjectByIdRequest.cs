using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Projects.GetProjectById;

/// <summary>
/// Represents a request to get a project identified by its <see cref="ProjectId"/>.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace containing the project.</param>
/// <param name="Id">The unique identifier of the project to be retrieved.</param>
public record GetProjectByIdRequest(WorkspaceId WorkspaceId, ProjectId Id) : IRequest<Result<ProjectResponse>>;
