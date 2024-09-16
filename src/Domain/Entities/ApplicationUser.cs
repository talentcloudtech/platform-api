using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser<UserId>
{
    /// <summary>
    /// Gets or sets the ID of the application user.
    /// </summary>
    public override UserId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the member profile associated with the user.
    /// </summary>
    public MemberProfile? MemberProfile { get; set; }

    /// <summary>
    /// Gets or sets the refresh tokens associated with the user.
    /// </summary>
    public List<RefreshToken>? RefreshTokens { get; set; }
}

/// <summary>
/// Represents an application role.
/// </summary>
public class ApplicationRole : IdentityRole<UserId>
{
    /// <summary>
    /// Gets or sets the ID of the application role.
    /// </summary>
    public override UserId Id { get; set; } = CustomNewId.GenerateStandardId();
}
