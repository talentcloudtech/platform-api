using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.ProjectMembers.GetAvailableMembers;

/// <summary>
/// Request to get available members that can be assigned to a project
/// </summary>
public record GetAvailableMembersRequest(int CurrentPage = 1, int PageSize = 10, string? Search = null) : IRequest<Result<PaginatedList<CommonUserResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the project ID
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }
}
