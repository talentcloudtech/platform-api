using System;
using System.Collections.Generic;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberRequests;

/// <summary>
/// Represents the response model for a member request in the application.
/// </summary>
public record MemberRequestResponse
{
    /// <summary>
    /// Gets the unique identifier for the member request.
    /// </summary>
    public MemberRequestId Id { get; init; }

    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the title of the member request.
    /// </summary>
    public string Title { get; init; } = null!;

    /// <summary>
    /// Gets or sets the description of the member request.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the start date of the member request.
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Gets or sets the end date of the member request.
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Gets or sets the expected budget for the member request.
    /// </summary>
    public string ExpectedBudget { get; init; } = null!;

    /// <summary>
    /// Gets or sets the number of minutes per day expected for the task.
    /// </summary>
    public string WorkHour { get; init; } = null!;

    /// <summary>
    /// Gets or sets the timezone relevant to the request.
    /// </summary>
    public string Timezone { get; init; } = null!;

    /// <summary>
    /// Gets or sets the list of skills required for the request.
    /// </summary>
    public List<MemberRequestSkill> RequiredSkills { get; init; } = [];

    /// <summary>
    /// Gets the current status of the member request.
    /// </summary>
    public MemberRequestStatus Status { get; init; }

    /// <summary>
    /// Gets the date when the member request was last updated.
    /// </summary>
    public DateTime? CreatedDate { get; init; }

    /// <summary>
    /// Gets or sets the workspace.
    /// </summary>
    public WorkspaceResponse? Workspace { get; set; }

    /// <summary>
    /// Gets the user who created this member request.
    /// </summary>
    public CommonUserResponse? CreatedByUser { get; set; }
}
