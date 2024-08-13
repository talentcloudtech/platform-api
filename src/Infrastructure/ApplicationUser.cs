using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Platform.Infrastructure;

/// <summary>
/// Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Gets or sets the ID of the application user.
    /// </summary>
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();

    // /// <summary>
    // /// Gets the list of project users.
    // /// </summary>
    // public List<ProjectUser> ProjectUsers { get; } = [];
}

/// <summary>
/// Represents an application role.
/// </summary>
public class ApplicationRole : IdentityRole<Guid>
{
    /// <summary>
    /// Gets or sets the ID of the application role.
    /// </summary>
    public override Guid Id { get; set; } = NewId.NextSequentialGuid();
}
