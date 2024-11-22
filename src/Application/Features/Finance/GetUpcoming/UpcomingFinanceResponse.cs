using System;
using System.Collections.Generic;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Finance.GetUpcoming;

/// <summary>
/// Details of an upcoming task's finance
/// </summary>
public class UpcomingFinanceResponse
{
    /// <summary>
    /// Gets or sets the task identifier
    /// </summary>
    public ProjectTaskId TaskId { get; set; }

    /// <summary>
    /// Gets or sets the task title
    /// </summary>
    public string TaskTitle { get; set; } = null!;

    /// <summary>
    /// Gets or sets the task status
    /// </summary>
    public TaskStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the task completion date
    /// </summary>
    public DateTime? CompletionDate { get; set; }

    /// <summary>
    /// Gets or sets the task cost
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Gets or sets the task assignee
    /// </summary>
    public List<CommonUserResponse> Assignees { get; set; } = new();
}
