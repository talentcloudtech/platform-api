using System;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Application.Features.Proposal;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal.CreateProposal;

/// <summary>
/// Represents a request to create a new proposal.
/// </summary>
public record CreateProposalRequest : IRequest<Result<ProposalResponse>>
{
    /// <summary>
    /// Gets or sets the task id associated with this proposal.
    /// </summary>
    [JsonIgnore]
    public ProjectTaskId TaskId { get; init; }

    /// <summary>
    /// Gets or sets the description of the proposal.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the estimated time to complete the proposal (in minutes).
    /// </summary>
    public int CostEstimation { get; set; }

    /// <summary>
    /// Gets or sets the start date of the proposal.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the proposal.
    /// </summary>
    public DateTime EndDate { get; set; }
};
