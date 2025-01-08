using Ardalis.Result;
using MediatR;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.SearchProposals;

/// <summary>
/// Represents a request to search for proposals.
/// </summary>
/// <param name="Statuses">The optional statuses to filter proposals by. If null, no filtering by status is applied.</param>
/// <param name="CurrentPage">The current page number for pagination. Defaults to 1.</param>
/// <param name="PageSize">The number of items per page for pagination. Defaults to 10.</param>
public record SearchProposalAppliedRequest(ProposalStatus[]? Statuses = null, int CurrentPage = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ProposalResponse>>>;
