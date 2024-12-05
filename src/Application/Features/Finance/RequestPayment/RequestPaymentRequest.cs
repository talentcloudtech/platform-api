using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Finance.RequestPayment;

/// <summary>
/// Request to request payments for tasks
/// </summary>
public record RequestPaymentRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the workspace identifier
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the list of task IDs to process payment for
    /// </summary>
    public List<ProjectTaskId> TaskIds { get; set; } = null!;
}
