using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMembers.SearchWorkspaceMembers;

/// <summary>
/// Request to search for workspace members.
/// </summary>
/// <param name="UserName">The optional name to filter workspace members by. If null, no filtering by name is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchWorkspaceMembersRequest(string? UserName = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<WorkspaceMemberResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; set; }
}
