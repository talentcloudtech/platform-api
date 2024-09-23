namespace Platform.Domain.Entities.Enums;

/// <summary>
/// Enum representing the different types of messages.
/// </summary>
public enum MessageType
{
    /// <summary>
    /// Represents a system message.
    /// </summary>
    System = 1,

    /// <summary>
    /// Represents a notification message.
    /// </summary>
    Notification = 2,

    /// <summary>
    /// Represents an announcement message.
    /// </summary>
    Announcement = 3,

    /// <summary>
    /// Represents a regular message.
    /// </summary>
    Message = 4,
}
