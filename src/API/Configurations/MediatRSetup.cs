using Microsoft.Extensions.DependencyInjection;
using Platform.Application.Common.Behaviors;

namespace Platform.Api.Configurations;

/// <summary>
/// Represents the setup class for MediatR.
/// </summary>
public static class MediatRSetup
{
    /// <summary>
    /// Adds the MediatR setup to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddMediatRSetup(this IServiceCollection services)
    {
        services.AddMediatR(
            (config) =>
            {
                config.RegisterServicesFromAssemblyContaining(typeof(IAssemblyMarker));
                config.AddOpenBehavior(typeof(ValidationResultPipelineBehavior<,>));
            }
        );

        return services;
    }
}
