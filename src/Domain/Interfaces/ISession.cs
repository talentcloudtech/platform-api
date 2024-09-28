using System;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Domain.Auth.Interfaces;

/// <summary>
/// Represents a session that provides information about the current user and the current time.
/// </summary>
public interface ICurrentSession
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Gets the role of the current user.
    /// </summary>
    public UserRole? Role { get; }

    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    public DateTime Now { get; }
}
