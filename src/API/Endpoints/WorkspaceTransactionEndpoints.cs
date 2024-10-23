using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for member requests.
/// </summary>
public class WorkspaceTransactionEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for member requests.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/transactions").WithTags("WorkspaceTransaction").RequireAuthorization();

        // group
        //     .MapGet(
        //         "/",
        //         async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceTransactionsRequest request) =>
        //         {
        //             var result = await mediator.Send(request with { WorkspaceId = workspaceId });
        //             return result.ToPlatformApiResult();
        //         }
        //     )
        //     .WithSummary("Searches for member requests.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, CreateWorkspaceTransactionRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new workspace transaction.");
    }
}
