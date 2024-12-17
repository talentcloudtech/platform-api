using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Extensions;
using Platform.Application.Features.Proposal;
using Platform.Application.Features.Proposal.CreateProposal;
using Platform.Application.Features.Proposal.SearchProposals;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Endpoints for project tasks.
/// </summary>
public class ProposalEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the project task endpoints.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/proposals/tasks/{taskId}").WithTags("Proposal").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchProposalsRequest request) =>
                {
                    var result = await mediator.Send(request with { TaskId = request.TaskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for proposals.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ProjectTaskId taskId, CreateProposalRequest request) =>
                {
                    var result = await mediator.Send(request with { TaskId = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new proposal.");
    }
}
