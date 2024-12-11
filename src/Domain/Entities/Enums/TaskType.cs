namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Represents the type of a task.
/// </summary>
public enum TaskType
{
    /// <summary>
    /// Represents a task that is internal for workspace.
    /// </summary>
    Internal = 1,

    /// <summary>
    /// Represents a task that is open for all users.
    /// </summary>
    Open = 2,
}
