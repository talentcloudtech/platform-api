using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common;
using Platform.Infrastructure;

namespace Platform.API.Configurations;

/// <summary>
/// Represents the application setup.
/// </summary>
public static class ApplicationSetup
{
    /// <summary>
    /// Adds the application setup to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());

        return services;
    }
}
