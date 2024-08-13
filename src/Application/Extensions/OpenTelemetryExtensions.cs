using System.Diagnostics;

namespace Platform.Application.Extensions;

/// <summary>
/// Provides extensions and constants for OpenTelemetry tracing within the application.
/// </summary>
public static class OpenTelemetryExtensions
{
    /// <summary>
    /// Gets the name of the service for OpenTelemetry tracing.
    /// </summary>
    public static string ServiceName { get; }

    /// <summary>
    /// Gets the version of the service for OpenTelemetry tracing.
    /// </summary>
    public static string ServiceVersion { get; }

    /// <summary>
    /// Gets the <see cref="ActivitySource"/> for OpenTelemetry tracing activities.
    /// </summary>
    public static ActivitySource ActivitySource { get; }

    /// <summary>
    /// Initializes static members of the <see cref="OpenTelemetryExtensions"/> class.
    /// </summary>
    static OpenTelemetryExtensions()
    {
        ServiceName = typeof(OpenTelemetryExtensions).Assembly.GetName().Name!;
        ServiceVersion = typeof(OpenTelemetryExtensions).Assembly.GetName().Version!.ToString();
        ActivitySource = new ActivitySource(ServiceName, ServiceVersion);
    }
}
