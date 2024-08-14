using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Infrastructure;

namespace Domain.Entities;

/// <summary>
/// Represents a project user.
/// </summary>
public class ProjectUser
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public ProjectId ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the project user role
    /// </summary>
    public ProjectUserRole Role { get; set; }

    /// <summary>
    /// Gets or sets the project
    /// </summary>
    public Project Project { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with project
    /// </summary>

    public ApplicationUser User { get; set; } = null!;
}
