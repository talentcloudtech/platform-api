using MassTransit;
using MassTransit.NewIdProviders;
using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common;
using Platform.Infrastructure;

namespace Platform.Api.Configurations;

public static class ApplicationSetup
{
    public static IServiceCollection AddApplicationSetup(this IServiceCollection services)
    {
        services.AddScoped<IContext, ApplicationDbContext>();
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());

        return services;
    }
}
