using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;

namespace Platform.Application.Features.Workspaces.SearchWorkspaces;

/// <summary>
/// Represents a request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="Name">The optional name to filter workspaces by. If null, no filtering by name is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchWorkspacesRequest(string? Name = null, int CurrentPage = 1, int PageSize = 10) : IRequest<Result<PaginatedList<WorkspaceResponse>>>;
