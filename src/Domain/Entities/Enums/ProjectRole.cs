namespace Platform.Domain.Enums;

/// <summary>
/// Enum representing the different roles in a project.
/// </summary>
public enum ProjectRole
{
    /// <summary>
    /// Represents a project admin role.
    /// </summary>
    ProjectAdmin = 1,

    /// <summary>
    /// Represents a project manager role.
    /// </summary>
    ProjectManager = 2,

    /// <summary>
    /// Represents a project owner role.
    /// </summary>
    ProjectOwner = 3,

    /// <summary>
    /// Represents a project member role.
    /// </summary>
    ProjectMember = 4
}