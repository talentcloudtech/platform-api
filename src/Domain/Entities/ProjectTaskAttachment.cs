using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a task attachment.
/// </summary>
public class ProjectTaskAttachment : AuditableEntity<ProjectTaskAttachmentId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the task attachment.
    /// </summary>
    public override ProjectTaskAttachmentId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public required ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the file name of the attachment.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the URL of the file.
    /// </summary>
    public required string FileURL { get; set; }

    /// <summary>
    /// Gets or sets the type of the file.
    /// </summary>
    public required string FileType { get; set; }

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public required long FileSize { get; set; }

    /// <summary>
    /// Gets or sets the project task associated with this attachment.
    /// </summary>
    public ProjectTask ProjectTask { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who uploaded this attachment.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;
}
