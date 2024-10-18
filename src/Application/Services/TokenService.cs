using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Platform.API.Configurations;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Interfaces;

namespace Platform.Application.Services;

/// <summary>
/// Service for managing tokens.
/// </summary>
/// <param name="userManager">The user manager.</param>
/// <param name="roleManager">The role manager.</param>
/// <param name="context">The context.</param>
/// <param name="jwtSettingsOptions">The JWT settings options.</param>
public class TokenService(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    IContext context,
    IOptions<JwtSettingsOptions> jwtSettingsOptions
) : ITokenService
{
    /// <inheritdoc/>
    public async Task<string> CreateAccessTokenAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariables.JWT_SECRET));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwtSettingsOptions.Value.AccessTokenExpirationMinutes);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, CustomNewId.GenerateStandardId()),
        };

        var userRoles = await userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var userClaims = await userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        foreach (var userRole in userRoles)
        {
            var role = await roleManager.FindByNameAsync(userRole);
            if (role != null)
            {
                var roleClaims = (await roleManager.GetClaimsAsync(role)).ToList();
                claims.AddRange(roleClaims);
            }
        }

        var token = new JwtSecurityToken(
            jwtSettingsOptions.Value.Issuer,
            jwtSettingsOptions.Value.Audience,
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inheritdoc/>
    public async Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user)
    {
        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = CustomNewId.GenerateStandardId(),
            Expires = DateTime.UtcNow.AddDays(jwtSettingsOptions.Value.RefreshTokenExpirationDays),
            Created = DateTime.UtcNow,
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return refreshToken;
    }
}
