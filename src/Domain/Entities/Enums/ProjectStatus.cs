namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of a project.
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// Represents a new project.
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Represents a project that has been started.
    /// </summary>
    Started = 2,

    /// <summary>
    /// Represents a completed project.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// Represents a canceled project.
    /// </summary>
    Canceled = 4,

    /// <summary>
    /// Represents a project on hold.
    /// </summary>
    OnHold = 5,
}
