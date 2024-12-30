namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the status of an open task
/// </summary>
public enum OpenTaskStatus
{
    /// <summary>
    /// Represents the status of an pending task
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Represents the status of an rejected task
    /// </summary>
    Rejected = 2,

    /// <summary>
    /// Represents the status of an published task
    /// </summary>
    Published = 3,

    /// <summary>
    /// Represents the status of an applied task
    /// </summary>
    Clarification = 4,

    /// <summary>
    /// Represents the status of an selected task
    /// </summary>
    Selected = 5,
}
