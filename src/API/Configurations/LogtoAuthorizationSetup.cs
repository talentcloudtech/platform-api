using System.Net.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure authorization with logto token.
/// </summary>
public static class LogtoAuthorizationSetup
{
    /// <summary>
    /// Configures Serilog as the logging provider for the specified IHostBuilder.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <returns>The IServiceCollection with persistence services added.</returns>
    public static IServiceCollection AddLogtoAuthorizationSetup(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var authority = configuration["JwtSettings:Authority"];
        var audience = configuration["JwtSettings:Audience"];

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var jwksUri = "https://rtuyk7.logto.app/oidc/jwks";
                        string json;

                        using (var httpClient = new HttpClient())
                        {
                            json = httpClient.GetStringAsync(jwksUri).Result;
                        }

                        var keys = new JsonWebKeySet(json).GetSigningKeys();
                        return keys;
                    }
                };
            });

        return services;
    }
}
