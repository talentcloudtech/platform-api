using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Platform.API.Endpoints;

namespace Platform.API.Configurations;

/// <summary>
/// Registers all endpoints in the application.
/// </summary>
public static class EndpointRegistration
{
    /// <summary>
    ///  Maps all endpoints in the application.
    /// </summary>
    /// <param name="app">The application endpoint route builder.</param>
    public static void MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t.IsClass && typeof(IEndpoints).IsAssignableFrom(t));

        foreach (var type in endpointTypes)
        {
            var method = type.GetMethod("MapEndpoints", BindingFlags.Public | BindingFlags.Static);
            method?.Invoke(null, new object[] { app });
        }
    }
}
