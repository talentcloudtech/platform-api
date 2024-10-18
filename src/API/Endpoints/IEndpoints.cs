using Microsoft.AspNetCore.Routing;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for the application.
/// </summary>
public interface IEndpoints
{
    /// <summary>
    /// Maps the endpoints for the application.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    static abstract void MapEndpoints(IEndpointRouteBuilder builder);
}
