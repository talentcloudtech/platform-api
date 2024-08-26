using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.API.Configurations;

/// <summary>
/// Extension methods for IServiceCollection to configure all IConfigurableOptions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Automatically registers all configuration options classes that implement
    /// <see cref="IConfigurableOptions"/> by scanning the assembly.
    /// </summary>
    /// <param name="services">The IServiceCollection to which the options will be added.</param>
    /// <param name="configuration">The IConfiguration from which the settings will be bound.</param>
    /// <returns>The IServiceCollection with the registered options.</returns>
    public static IServiceCollection ConfigureAllOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var optionsTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i == typeof(IConfigurableOptions)));

        foreach (var optionsType in optionsTypes)
        {
            var configureMethod = typeof(OptionsConfigurationServiceCollectionExtensions)
                .GetMethod("Configure", new[] { typeof(IServiceCollection), typeof(IConfiguration) })
                ?.MakeGenericMethod(optionsType);

            var sectionName = optionsType.Name.Replace("Options", "");
            var configurationSection = configuration.GetSection(sectionName);

            configureMethod?.Invoke(null, new object[] { services, configurationSection });
        }

        return services;
    }

    /// <summary>
    /// Marker interface for configuration options classes.
    /// Implement this interface in any class that represents
    /// configuration settings bound from the appsettings.json file.
    /// </summary>
    public interface IConfigurableOptions { }
}
