using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Domain.Services.Logto;

namespace Platform.Domain.Interfaces;

/// <summary>
/// Represents a service for interacting with the Logto API.
/// </summary>
public interface ILogtoService
{
    /// <summary>
    /// Gets the user by ID.
    /// </summary>
    /// <param name="userId">The ID of the user to get.</param>
    /// <returns>The user with the specified ID.</returns>
    Task<LogtoUser?> GetUserAsync(string userId);

    /// <summary>
    /// Gets the users.
    /// </summary>
    /// <returns>The users.</returns>
    Task<IEnumerable<LogtoUser>?> GetUsersAsync();

    /// <summary>
    /// Updates the user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The user to update.</param>
    /// <returns>The updated user.</returns>
    Task<LogtoUser?> UpdateUserAsync(string userId, LogtoUser user);
}
