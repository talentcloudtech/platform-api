using System.Collections.Generic;
using System.Text.Json.Serialization;
using Ardalis.Result;
using MediatR;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Workspaces.UpdateWorkspace;

/// <summary>
/// Represents a request to create a workspace.
/// </summary>
public record UpdateWorkspaceRequest : IRequest<Result<WorkspaceResponse>>
{
    /// <summary>
    /// Gets or sets the identifier of the workspace.
    /// </summary>
    [JsonIgnore]
    public WorkspaceId Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the workspace.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the contact email for the workspace.
    /// </summary>
    public string ContactEmail { get; set; } = null!;

    /// <summary>
    /// Gets or sets the URLs associated with the workspace.
    /// </summary>
    public string[] Urls { get; set; } = [];

    /// <summary>
    /// Gets or sets the address for the workspace.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the logo for the workspace.
    /// </summary>
    public string? Logo { get; set; }
}
