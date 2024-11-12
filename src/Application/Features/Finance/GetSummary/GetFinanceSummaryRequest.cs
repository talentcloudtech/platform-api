using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Finance.GetSummary;

/// <summary>
/// Request to get finance summary for a workspace
/// </summary>
public class GetFinanceSummaryRequest : IRequest<Result<FinanceSummaryResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    public WorkspaceId WorkspaceId { get; set; }

    /// <summary>
    /// Gets or sets the optional project identifier for filtering.
    /// </summary>
    public ProjectId? ProjectId { get; set; }

    /// <summary>
    /// Gets or sets whether to filter by task closed status.
    /// </summary>
    public bool? IsClosed { get; set; }
}
