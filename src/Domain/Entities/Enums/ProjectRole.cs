namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Enum representing the different roles in a project.
/// </summary>
public enum ProjectRole
{
    /// <summary>
    /// Represents a project owner role.
    /// </summary>
    ProjectOwner = 1,

    /// <summary>
    /// Represents a project admin role.
    /// </summary>
    ProjectAdmin = 2,

    /// <summary>
    /// Represents a project manager role.
    /// </summary>
    ProjectManager = 3,

    /// <summary>
    /// Represents a project member role.
    /// </summary>
    ProjectMember = 4,
}
