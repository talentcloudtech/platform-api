using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Contracts.CreateContract;
using Platform.Application.Features.Contracts.DeleteContract;
using Platform.Application.Features.Contracts.GetContractById;
using Platform.Application.Features.Contracts.SearchContracts;
using Platform.Application.Features.Contracts.UpdateContract;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for contracts.
/// </summary>
public class ContractEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for contracts.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/contracts").WithTags("Contract").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, WorkspaceId workSpaceId, [AsParameters] SearchContractsRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for contracts.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, WorkspaceId workSpaceId, ContractId id) =>
                {
                    var result = await mediator.Send(new GetContractByIdRequest(workSpaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a contract by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workSpaceId, CreateContractRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new contract.");

        group
            .MapPatch(
                "{id}",
                async (IMediator mediator, WorkspaceId workSpaceId, ContractId id, UpdateContractRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workSpaceId, Id = id });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a contract by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, ContractId id) =>
                {
                    var result = await mediator.Send(new DeleteContractRequest(workspaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a contract by its unique identifier.");
    }
}
