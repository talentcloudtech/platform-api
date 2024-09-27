using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.GetUserWorkspaceRole;

/// <summary>
/// Response for workspace member role.
/// </summary>
public class WorkspaceMemberRoleResponse
{
    /// <summary>
    /// The role of the workspace member.
    /// </summary>
    public WorkspaceRole Role { get; set; }
}
