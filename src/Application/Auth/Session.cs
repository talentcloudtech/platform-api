using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Auth;

/// <summary>
/// Represents the session information for the current user, including the user's ID, role, and the current time.
/// </summary>
public class CurrentSession : ICurrentSession
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public UserId UserId { get; private init; }

    /// <summary>
    /// Gets the role of the current user.
    /// </summary>
    public UserRole? Role { get; set; }

    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentSession"/> class, extracting the user ID and role from the current HTTP context.
    /// </summary>
    /// <param name="httpContextAccessor">The accessor for the current HTTP context.</param>
    public CurrentSession(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);
        var roleClaim = user?.FindFirst(ClaimTypes.Role);

        if (nameIdentifier != null)
        {
            UserId = nameIdentifier.Value;
        }
        if (roleClaim != null)
        {
            Role = UserRole.FromString(roleClaim.Value);
        }
    }
}
