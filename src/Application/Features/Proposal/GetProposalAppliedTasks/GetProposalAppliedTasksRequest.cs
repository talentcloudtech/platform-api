using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.GetProposalAppliedTasks;

/// <summary>
/// Represents a request to search for open tasks.
/// </summary>
/// <param name="Search">The optional search to filter tasks by. If null, no filtering by search is applied.</param>
/// <param name="SortOrder">The optional sort order to filter tasks by. If null, no filtering by sort order is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record GetProposalAppliedTasksRequest(string? Search = null, string SortOrder = "desc", int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ProposalResponse>>>;
