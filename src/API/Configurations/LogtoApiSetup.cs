using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Infrastructure.LogtoSdk;
using Platform.Infrastructure.LogtoSdk.Users;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure Logto API.
/// </summary>
public static class LogtoApiSetup
{
    /// <summary>
    /// Adds the Logto API setup to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <param name="sectionName">The name of the section in the configuration file.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddLogtoApi(this IServiceCollection services, IConfiguration configuration, string sectionName = "LogtoApi")
    {
        services.Configure<LogtoApiOptions>(configuration.GetSection(sectionName));
        services.PostConfigure<LogtoApiOptions>(opt =>
        {
            if (string.IsNullOrWhiteSpace(opt.Url))
            {
                throw new KeyNotFoundException("Logto API configuration: missing URL.");
            }
            if (string.IsNullOrWhiteSpace(opt.ClientId))
            {
                throw new KeyNotFoundException("Logto API configuration: missing client id.");
            }
            if (string.IsNullOrWhiteSpace(opt.ClientSecret))
            {
                throw new KeyNotFoundException("Logto API configuration: missing client secret.");
            }
        });

        return services;
    }
}
