using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Finance.GetSummary;
using Platform.Application.Features.Finance.GetUpcoming;
using Platform.Application.Features.Finance.ReleasePayment;
using Platform.Application.Features.Finance.RequestPayment;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the finance endpoints
/// </summary>
public class FinanceEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the finance endpoints
    /// </summary>
    /// <param name="builder">The endpoint route builder</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/{workspaceId}/finance").WithTags("Finance").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] GetUpcomingFinanceRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get upcoming finance details");

        group
            .MapGet(
                "/summary",
                async (IMediator mediator, [AsParameters] GetFinanceSummaryRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get finance summary");

        group
            .MapGet(
                "/upcoming",
                async (IMediator mediator, [AsParameters] GetUpcomingFinanceRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Get upcoming finance details");

        group
            .MapPost(
                "/release-payment",
                async (IMediator mediator, WorkspaceId workspaceId, ReleasePaymentRequest request) =>
                {
                    var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Release payments for tasks")
            .RequireAuthorization(policy => policy.RequireRole("Admin", "WorkspaceOwner"));

        group.MapPost(
            "/request-payment",
            async (IMediator mediator, WorkspaceId workspaceId, RequestPaymentRequest request) =>
            {
                var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                return result.ToPlatformApiResult();
            }
        );
    }
}
