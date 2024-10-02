using System.Diagnostics;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Platform.Application.Extensions;

namespace Platform.Api.Configurations;

/// <summary>
/// Represents the OpenTelemetry setup class.
/// </summary>
public static class OpenTelemetrySetup
{
    /// <summary>
    /// Adds the OpenTelemetry setup.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static void AddOpenTelemetrySetup(this WebApplicationBuilder builder)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        builder
            .Services.AddOpenTelemetry()
            .ConfigureResource(
                (rb) =>
                    rb.AddService(serviceName: OpenTelemetryExtensions.ServiceName, serviceVersion: OpenTelemetryExtensions.ServiceVersion)
                        .AddTelemetrySdk()
                        .AddEnvironmentVariableDetector()
            )
            .WithTracing(telemetry =>
            {
                telemetry
                    .AddSource(OpenTelemetryExtensions.ServiceName)
                    .AddAspNetCoreInstrumentation(o =>
                    {
                        o.RecordException = true;
                    })
                    .AddHttpClientInstrumentation(o =>
                    {
                        o.RecordException = true;
                    })
                    .AddAzureMonitorTraceExporter()
                    .AddOtlpExporter();
            })
            .WithMetrics(telemetry =>
            {
                telemetry
                    .AddMeter("Microsoft.AspNetCore.Hosting")
                    .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                    .AddView(
                        "http-server-request-duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries = new double[] { 0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10 },
                        }
                    )
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAzureMonitorMetricExporter()
                    .AddOtlpExporter();
            });
    }
}
