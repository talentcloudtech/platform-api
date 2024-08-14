using System.Security.Claims;
using Platform.Domain.Entities.Common;
using ISession = Platform.Domain.Auth.Interfaces.ISession;

namespace Platform.Application.Auth;

/// <summary>
/// Represents the session information for the current user, including the user's ID and the current time.
/// </summary>
public class Session : ISession
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public UserId UserId { get; private init; }

    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Initializes a new instance of the <see cref="Session"/> class, extracting the user ID from the current HTTP context.
    /// </summary>
    /// <param name="httpContextAccessor">The accessor for the current HTTP context.</param>
    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;

        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (nameIdentifier != null)
        {
            UserId = new Guid(nameIdentifier.Value);
        }
    }
}
