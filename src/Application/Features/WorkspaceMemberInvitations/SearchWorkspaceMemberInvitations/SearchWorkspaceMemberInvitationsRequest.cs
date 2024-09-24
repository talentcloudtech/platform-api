using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.WorkspaceMemberInvitations.SearchWorkspaceMemberInvitations;

/// <summary>
/// Request to search for workspace members.
/// </summary>
/// <param name="Email">The optional email to filter workspace member invitations by. If null, no filtering by email is applied.</param>
/// <param name="WorkspaceIds">The optional workspace IDs to filter workspace member invitations by. If null, no filtering by workspace ID is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchWorkspaceMemberInvitationsRequest(WorkspaceId[]? WorkspaceIds = null, string? Email = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<WorkspaceMemberInvitationResponse>>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    public WorkspaceId[] WorkspaceIds { get; init; } = WorkspaceIds ?? [];
}
