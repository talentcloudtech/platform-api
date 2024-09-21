using System;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceMembers;

/// <summary>
/// Represents a workspace member.
/// </summary>
public class WorkspaceMemberResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace member.
    /// </summary>
    public WorkspaceMemberId Id { get; set; }

    /// <summary>
    /// Gets or sets the user identifier associated with this workspace member.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the workspace identifier associated with this workspace member.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the role of the member in the workspace.
    /// </summary>
    public WorkspaceRole Role { get; set; }

    /// <summary>
    /// Gets or sets the date when the workspace member was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this workspace member.
    /// </summary>
    public UserId CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the workspace member was last updated. Null if never updated.
    /// </summary>
    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this workspace member. Null if never updated.
    /// </summary>
    public UserId? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the user details.
    /// </summary>
    public CommonUserResponse? User { get; set; }
}
