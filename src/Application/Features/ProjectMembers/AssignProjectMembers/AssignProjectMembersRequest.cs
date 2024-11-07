using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers.AssignProjectMembers;

/// <summary>
/// Request to assign multiple users to a project
/// </summary>
public record AssignProjectMembersRequest : IRequest<Result>
{
    /// <summary>
    /// Gets or sets the project ID
    /// </summary>
    [JsonIgnore]
    public ProjectId ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the workspace ID
    /// </summary>
    [JsonIgnore]
    public WorkspaceId WorkspaceId { get; init; }

    /// <summary>
    /// Gets or sets the list of user IDs to assign
    /// </summary>
    public ICollection<UserId> UserIds { get; set; } = null!;

    /// <summary>
    /// Gets or sets the role for the assigned users
    /// </summary>
    public ProjectRole Role { get; set; }
}
