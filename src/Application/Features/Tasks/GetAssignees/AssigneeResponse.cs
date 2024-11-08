using System;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Tasks.GetAssignees;

/// <summary>
/// Response model for task assignee
/// </summary>
public class AssigneeResponse
{
    /// <summary>
    /// Gets or sets the assigned user details
    /// </summary>
    public required CommonUserResponse AssignedUser { get; init; }

    /// <summary>
    /// Gets or sets the user who assigned the task
    /// </summary>
    public required CommonUserResponse AssignedByUser { get; init; }

    /// <summary>
    /// Gets or sets when the task was assigned
    /// </summary>
    public required DateTime AssignedDate { get; init; }
}
