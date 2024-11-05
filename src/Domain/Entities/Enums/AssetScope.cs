namespace Domain.Entities.Enums;

/// <summary>
/// Asset scope enum
/// </summary>
public enum AssetScope
{
    /// <summary>
    /// Asset belongs to a workspace.
    /// </summary>
    Workspace,

    /// <summary>
    /// Asset belongs to a project.
    /// </summary>
    Project,

    /// <summary>
    /// Asset belongs to a task.
    /// </summary>
    Task
}