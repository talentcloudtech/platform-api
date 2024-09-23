using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMembers.UpdateWorkspaceMember;

/// <summary>
/// Request to update a workspace member.
/// </summary>
public record UpdateWorkspaceMemberRequest : IRequest<Result<WorkspaceMemberResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier associated with this workspace member.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the user identifier associated with this workspace member.
    /// </summary>
    [JsonIgnore]
    public UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the role of the member in the workspace.
    /// </summary>
    public WorkspaceRole Role { get; init; }
}
