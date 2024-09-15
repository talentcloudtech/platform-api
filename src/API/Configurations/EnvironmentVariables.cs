using System;

namespace Platform.Api.Configurations;

/// <summary>
/// Provides access to environment variables.
/// </summary>
public static class EnvironmentVariables
{
    /// <summary>
    /// Gets the frontend URL from environment variables.
    /// </summary>
    public static string FrontendUrl => Environment.GetEnvironmentVariable("FRONTEND_URL") ?? throw new ArgumentNullException("FRONTEND_URL");

    /// <summary>
    /// Gets the backend URL from environment variables.
    /// </summary>
    public static string JWT_SECRET => Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new ArgumentNullException("JWT_SECRET");

    /// <summary>
    /// Gets the communication services connection string from environment variables.
    /// </summary>
    public static string CommunicationServicesConnectionString =>
        Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING")
        ?? throw new ArgumentNullException("COMMUNICATION_SERVICES_CONNECTION_STRING");

    /// <summary>
    /// Gets the backend URL from environment variables.
    /// </summary>
    public static string BackendUrl => Environment.GetEnvironmentVariable("BACKEND_URL") ?? throw new ArgumentNullException("BACKEND_URL");
}
