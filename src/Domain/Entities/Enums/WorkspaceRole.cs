namespace Platform.Domain.Enums;

/// <summary>
/// Enum representing the different roles of a workspace member.
/// </summary>
public enum WorkspaceRole
{
    /// <summary>
    /// Represents the owner of the workspace.
    /// </summary>
    WorkspaceOwner = 1,

    /// <summary>
    /// Represents a member of the workspace.
    /// </summary>
    WorkspaceMember = 2
}