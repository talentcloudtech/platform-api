using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides extension methods to configure response compression for the application.
/// </summary>
public static class CompressionSetup
{
    /// <summary>
    /// Adds and configures response compression services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The IServiceCollection with the compression services added.</returns>
    public static IServiceCollection AddCompressionSetup(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

        services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

        return services;
    }
}
