using System.Threading.Tasks;
using Platform.Domain.Entities;

namespace Platform.Domain.Interfaces;

/// <summary>
/// Defines the contract for a token service.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The JWT token as a string.</returns>
    Task<string> CreateAccessTokenAsync(ApplicationUser user);

    /// <summary>
    /// Creates a refresh token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is created.</param>
    /// <returns>The refresh token.</returns>
    Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user);
}
