using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.ApproveRejectProposal;

/// <summary>
/// Request to update a proposal.
/// </summary>
public record ApproveRejectProposalRequest : IRequest<Result<ProposalResponse>>
{
    /// <summary>
    /// Gets the proposal identifier.
    /// </summary>
    [JsonIgnore]
    public ProposalId Id { get; init; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public ProposalStatus Status { get; set; }
}
