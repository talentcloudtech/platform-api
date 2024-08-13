namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of a project.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// Represents a new project.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Represents a project in progress.
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Represents a completed project.
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Represents a canceled project.
    /// </summary>
    Canceled = 3
}
