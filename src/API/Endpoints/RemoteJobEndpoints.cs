using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.RemoteJobs.SearchRemoteJobs;

namespace Platform.API.Endpoints;

/// <summary>
/// Endpoints for remote jobs.
/// </summary>
public class RemoteJobEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the remote job endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/remote-jobs").WithTags("RemoteJob").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchRemoteJobsRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for remote jobs.");

        // Add other endpoints (GET by ID, POST, PATCH, DELETE) here as needed
    }
}
