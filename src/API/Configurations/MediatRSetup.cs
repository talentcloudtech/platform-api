using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common.Behaviors;

namespace Platform.Api.Configurations;

public static class MediatRSetup
{
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR(
            (config) =>
            {
                config.RegisterServicesFromAssemblyContaining(typeof(Platform.Application.IAssemblyMarker));
                config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
            }
        );

        return services;
    }
}
