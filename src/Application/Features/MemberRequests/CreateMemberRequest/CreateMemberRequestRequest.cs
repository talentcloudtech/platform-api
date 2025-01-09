using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberRequests.CreateMemberRequest;

/// <summary>
/// Represents a request to create a member request.
/// </summary>
public record CreateMemberRequestRequest : IRequest<Result<MemberRequestResponse>>
{
    /// <summary>
    /// Gets or sets the workspace identifier.
    /// </summary>
    [JsonIgnore]
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
    public List<SkillId> RequiredSkills { get; init; } = null!;
}
