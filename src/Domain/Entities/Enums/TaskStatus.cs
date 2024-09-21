namespace Platform.Domain.Enums;

/// <summary>
/// Enum representing the different statuses of a task.
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Represents a task that needs to be done.
    /// </summary>
    ToDo = 1,

    /// <summary>
    /// Represents a task that is currently in progress.
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// Represents a task that is under review.
    /// </summary>
    Review = 3,

    /// <summary>
    /// Represents a task that has been completed.
    /// </summary>
    Completed = 4,

    /// <summary>
    /// Represents a task that has been cancelled.
    /// </summary>
    Cancelled = 5,
}
