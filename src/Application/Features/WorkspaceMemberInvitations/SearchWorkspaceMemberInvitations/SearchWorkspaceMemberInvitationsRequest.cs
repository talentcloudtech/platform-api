using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.SearchWorkspaceMemberInvitations;

/// <summary>
/// Request to search for workspace members.
/// </summary>
/// <param name="Email">The optional email to filter workspace member invitations by. If null, no filtering by email is applied.</param>
/// <param name="SortBy">The optional sort by to filter workspace member invitations by. If null, no filtering by sort is applied.</param>
/// <param name="SortOrder">The optional sort order to filter workspace member invitations by. If null, no filtering by sort order is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchWorkspaceMemberInvitationsRequest(
    string? Email = null,
    string SortBy = nameof(WorkspaceMemberInvitation.CreatedDate),
    string SortOrder = "desc",
    int CurrentPage = 1,
    int PageSize = 10
) : IRequest<Result<PaginatedList<WorkspaceMemberInvitationResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }
}
