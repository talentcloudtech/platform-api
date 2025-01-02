using JobBoard.Application.Features.Proposal.GetProposal;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Extensions;
using Platform.Application.Features.Proposal;
using Platform.Application.Features.Proposal.ApproveRejectProposal;
using Platform.Application.Features.Proposal.CreateProposal;
using Platform.Application.Features.Proposal.GetProposalAppliedTasks;
using Platform.Application.Features.Proposal.SearchProposals;
using Platform.Application.Features.Proposal.UpdateProposal;
using Platform.Domain.Auth.Interfaces;
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
        var group = builder.MapGroup("api/proposals").WithTags("Proposal").RequireAuthorization();

        group
            .MapGet(
                "/{proposalId}",
                async (IMediator mediator, ProposalId proposalId) =>
                {
                    var result = await mediator.Send(new GetProposalRequest(proposalId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Gets a proposal.");

        group
            .MapGet(
                "/tasks/{taskId}",
                async (IMediator mediator, ProjectTaskId taskId, [AsParameters] SearchProposalsRequest request) =>
                {
                    var result = await mediator.Send(request with { TaskId = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for proposals.");

        group
            .MapPost(
                "/tasks/{taskId}",
                async (IMediator mediator, ProjectTaskId taskId, CreateProposalRequest request) =>
                {
                    var result = await mediator.Send(request with { TaskId = taskId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new proposal.");

        group
            .MapPatch(
                "/{proposalId}",
                async (IMediator mediator, ProposalId proposalId, UpdateProposalRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = proposalId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a proposal.");

        group
            .MapPost(
                "/{proposalId}/accept-reject",
                async (IMediator mediator, ProposalId proposalId, ApproveRejectProposalRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = proposalId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a proposal.");

        group
            .MapPost(
                "/applied",
                async (IMediator mediator, ICurrentSession currentUser) =>
                {
                    var result = await mediator.Send(new GetProposalAppliedTasksRequest());
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a proposal.");
    }
}
