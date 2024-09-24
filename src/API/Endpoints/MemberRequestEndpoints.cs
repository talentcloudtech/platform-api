using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.MemberRequests.CreateMemberRequest;
using Platform.Application.Features.MemberRequests.GetMemberRequestById;
using Platform.Application.Features.MemberRequests.SearchMemberRequests;
using Platform.Application.Features.MemberRequests.UpdateMemberRequest;
using Platform.Domain.Entities.Common;

namespace Platform.Api.Endpoints;

/// <summary>
/// Represents the endpoints for member requests.
/// </summary>
public static class MemberRequestEndpoints
{
    /// <summary>
    /// Maps the endpoints for member requests.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapMemberRequestEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/workspaces/{workspaceId}/memberrequests").WithTags("MemberRequest").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchMemberRequestsRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for member requests.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, MemberRequestId id) =>
                {
                    var result = await mediator.Send(new GetMemberRequestByIdRequest(workspaceId, id));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a member request by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, CreateMemberRequestRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new member request.");

        group
            .MapPatch(
                "{id}",
                async (IMediator mediator, WorkspaceId workspaceId, MemberRequestId id, UpdateMemberRequestRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId, Id = id });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a member request by its unique identifier.");
    }
}
