using System;
using System.Collections.Generic;
using Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Projects;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Proposal;

/// <summary>
/// Represents a proposal response in the system.
/// </summary>
public class ProposalResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the project task.
    /// </summary>
    public ProposalId Id { get; init; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the cost associated with the task.
    /// </summary>
    public decimal? CostEstimate { get; set; }

    /// <summary>
    /// Gets or sets the start date of the task.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the due date of the task.
    /// </summary>
    public UserId? UserId { get; set; }

    /// <summary>
    /// Gets or sets the status of the task.
    /// </summary>
    public ProposalStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the date when the task was created.
    /// </summary>
    public DateTime CreatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who created this task.
    /// </summary>
    public UserId CreatedBy { get; init; }

    /// <summary>
    /// Gets or sets the date when the task was last updated.
    /// </summary>
    public DateTime? UpdatedDate { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the user who last updated this task.
    /// </summary>
    public UserId? UpdatedBy { get; init; }

    /// <summary>
    /// Gets or sets the user who created this task.
    /// </summary>
    public CommonUserResponse? CreatedByUser { get; set; }
}
