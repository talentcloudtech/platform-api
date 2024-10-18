using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Platform.API.Configurations;

/// <summary>
/// Provides extension methods to configure logging for the application using Serilog.
/// </summary>
public static class LoggingSetup
{
    /// <summary>
    /// Configures Serilog as the logging provider for the specified IHostBuilder.
    /// </summary>
    /// <param name="host">The IHostBuilder to configure.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <returns>The IHostBuilder with Serilog configured.</returns>
    public static IHostBuilder UseLoggingSetup(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog(
            (_, _, lc) =>
            {
                lc.ReadFrom.Configuration(configuration);
            }
        );

        return host;
    }
}
