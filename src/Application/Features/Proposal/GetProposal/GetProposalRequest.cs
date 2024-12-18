using Ardalis.Result;
using MediatR;
using Platform.Application.Features.Proposal;
using Platform.Domain.Entities.Common;

namespace JobBoard.Application.Features.Proposal.GetProposal;

/// <summary>
/// Request to get a proposal by ID.
/// </summary>
public record GetProposalRequest(ProposalId Id) : IRequest<Result<ProposalResponse>>;
