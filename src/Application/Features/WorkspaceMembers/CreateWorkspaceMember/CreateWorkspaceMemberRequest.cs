using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Enums;

namespace Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;

/// <summary>
/// Represents a request to create a workspace member.
/// </summary>
public record CreateWorkspaceMemberRequest : IRequest<Result<WorkspaceMemberResponse>>
{
    /// <summary>
    /// Gets or sets the workspace ID.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public UserId UserId { get; init; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public WorkspaceRole Role { get; init; }
}
