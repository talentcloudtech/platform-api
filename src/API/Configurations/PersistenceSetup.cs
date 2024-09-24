using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Auth;
using Platform.Application.Services;
using Platform.Application.Services.Email;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Interfaces;
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
        services.AddScoped<ICurrentSession, CurrentSession>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddTransient<IEmailService, AzureEmailService>();
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
        services.AddTransient<IEmailSender, EmailSender>();

        services.AddHostedService<ApplicationDbInitializer>();
        services.AddDbContextPool<ApplicationDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).UseExceptionProcessor();
        });

        return services;
    }
}
