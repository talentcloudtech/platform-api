using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceAssets.SearchWorkspaceAssets;

/// <summary>
/// Request to search workspace assets
/// </summary>
public record SearchWorkspaceAssetsRequest(
    ProjectId? ProjectId = null,
    ProjectTaskId? TaskId = null,
    int CurrentPage = 1,
    int PageSize = 10,
    string SortBy = "CreatedDate",
    string SortOrder = "desc"
) : IRequest<Result<PaginatedList<WorkspaceAssetResponse>>>
{
    /// <summary>
    /// The workspace ID to filter by
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }
}
