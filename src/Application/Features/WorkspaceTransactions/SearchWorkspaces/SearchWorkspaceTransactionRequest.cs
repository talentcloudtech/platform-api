using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceTransactions.SearchWorkspaceTransactions;

/// <summary>
/// Represents a request to search for workspaces with optional filtering and pagination.
/// </summary>
/// <param name="Search">The optional search to filter workspaces by. If null, no filtering by search is applied.</param>
/// <param name="SortBy">The optional sort by to filter workspaces by. If null, no filtering by sort is applied.</param>
/// <param name="SortOrder">The optional sort order to filter workspaces by. If null, no filtering by sort order is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
/// <param name="ProjectId">The optional project ID for filtering transactions. If null, no filtering by project is applied.</param>
public record SearchWorkspaceTransactionRequest(
    string? Search = null,
    string SortBy = nameof(WorkspaceTransaction.CreatedDate),
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10,
    ProjectId? ProjectId = null
) : IRequest<Result<PaginatedList<WorkspaceTransactionResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; set; }
}
