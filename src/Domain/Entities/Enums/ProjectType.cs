namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the type of a project.
/// </summary>
public enum ProjectType
{
    /// <summary>
    /// Represents a project that is a self managed project.
    /// </summary>
    SelfManaged = 1,

    /// <summary>
    /// Represents a project that is a TC managed project.
    /// </summary>
    TCManaged = 2,
}
