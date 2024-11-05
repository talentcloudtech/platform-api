namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Defines the scope of an asset in the workspace hierarchy
/// </summary>
public enum AssetScope
{
    /// <summary>
    /// Asset belongs to a workspace
    /// </summary>
    Workspace,

    /// <summary>
    /// Asset belongs to a project
    /// </summary>
    Project,

    /// <summary>
    /// Asset belongs to a task
    /// </summary>
    Task,
}
