using Platform.Domain.Entities.Common;
using System.Collections.Generic;

namespace Platform.Application.Features.Finance.GetSummary;

/// <summary>
/// Response containing finance summary information
/// </summary>
public class FinanceSummaryResponse
{
    /// <summary>
    /// Gets or sets the total cost of all tasks
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Gets or sets the total count of tasks
    /// </summary>
    public int TaskCount { get; set; }
}
