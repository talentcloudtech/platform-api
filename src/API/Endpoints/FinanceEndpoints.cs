using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Finance.GetSummary;

namespace Platform.API.Endpoints;

/// <summary>
/// Maps the gig invitation endpoints.
/// </summary>
public class FinanceEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the gig invitation endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/{workspaceId}/finance").WithTags("Finance").RequireAuthorization();

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
    }
}
