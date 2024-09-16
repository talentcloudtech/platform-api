using System;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a refresh token entity.
/// </summary>
public class RefreshToken : Entity<RefreshTokenId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the refresh token.
    /// </summary>
    public override RefreshTokenId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the token value.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Gets or sets the expiration date and time of the refresh token.
    /// </summary>
    public required DateTime Expires { get; set; }

    /// <summary>
    /// Gets a value indicating whether the refresh token is expired.
    /// </summary>
    public bool IsExpired => DateTime.UtcNow > Expires;

    /// <summary>
    /// Gets or sets the creation date and time of the refresh token.
    /// </summary>
    public required DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the refresh token was revoked.
    /// </summary>
    public DateTime? Revoked { get; set; }

    /// <summary>
    /// Gets a value indicating whether the refresh token is active.
    /// </summary>
    public bool IsActive => Revoked == null && !IsExpired;

    /// <summary>
    /// Gets or sets the user identifier associated with the refresh token.
    /// </summary>
    public required UserId UserId { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the refresh token.
    /// </summary>
    public ApplicationUser? User { get; set; }
}
