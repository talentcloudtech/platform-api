using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.UpdateProposal;

/// <summary>
/// Request to update a proposal.
/// </summary>
public record UpdateProposalRequest : IRequest<Result<ProposalResponse>>
{
    /// <summary>
    /// Gets the proposal identifier.
    /// </summary>
    [JsonIgnore]
    public ProposalId Id { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cost estimation.
    /// </summary>
    public decimal? CostEstimation { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public ProposalStatus? Status { get; set; }
}
