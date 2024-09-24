using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberRequests.UpdateMemberRequest;

/// <summary>
/// Represents a request to update a member request's details.
/// </summary>
public record UpdateMemberRequestRequest : IRequest<Result<MemberRequestResponse>>
{
    /// <summary>
    /// Gets the unique identifier of the member request to be updated.
    /// </summary>
    [JsonIgnore]
    public MemberRequestId Id { get; init; }

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
    public decimal ExpectedBudget { get; init; }

    /// <summary>
    /// Gets or sets the number of minutes per day expected for the task.
    /// </summary>
    public int MinutesPerDay { get; init; }

    /// <summary>
    /// Gets or sets the timezone relevant to the request.
    /// </summary>
    public string Timezone { get; init; } = null!;

    /// <summary>
    /// Gets or sets the list of skills required for the request.
    /// </summary>
    public List<string> RequiredSkills { get; init; } = new();

    /// <summary>
    /// Gets the current status of the member request.
    /// </summary>
    public MemberRequestStatus Status { get; init; }
}
