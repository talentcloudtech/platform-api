using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Auth;
using Platform.Infrastructure;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure persistence and database-related services for the application.
/// </summary>
public static class PersistenceSetup
{
    /// <summary>
    /// Adds and configures persistence services, including Entity Framework and database initialization, to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <returns>The IServiceCollection with persistence services added.</returns>
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<Domain.Auth.Interfaces.ISession, Session>();
        services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            o.UseExceptionProcessor();
        });

        return services;
    }
}
