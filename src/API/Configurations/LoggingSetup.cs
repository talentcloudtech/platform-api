using Serilog;

namespace Platform.Api.Configurations;

public static class LoggingSetup
{
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
