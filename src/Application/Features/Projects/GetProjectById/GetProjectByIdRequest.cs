using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Projects.GetProjectById;

/// <summary>
/// Represents a request to get a project identified by its <see cref="ProjectId"/>.
/// </summary>
/// <param name="Id">The unique identifier of the project to be deleted.</param>
public record GetProjectByIdRequest(ProjectId Id) : IRequest<Result<ProjectResponse>>;
