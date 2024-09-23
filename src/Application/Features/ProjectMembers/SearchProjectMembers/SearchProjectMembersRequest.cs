using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectMembers.SearchProjectMembers;

/// <summary>
/// Request to search for project members.
/// </summary>
/// <param name="UserName">The optional name to filter project members by. If null, no filtering by name is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchProjectMembersRequest(string? UserName = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ProjectMemberResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }
}
