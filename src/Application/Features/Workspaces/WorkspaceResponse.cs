using System;
using System.Collections.Generic;
using Platform.Application.Common.Responses;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces;

/// <summary>
/// Represents the response model for a workspace in the application.
/// </summary>
public record WorkspaceResponse
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace.
    /// </summary>
    public WorkspaceId Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the workspace.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the contact email for the workspace.
    /// </summary>
    public string? ContactEmail { get; set; } = null!;

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

    /// <summary>
    /// Gets or sets a value indicating whether the workspace is the default workspace.
    /// </summary>
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Gets or sets the date when the workspace was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the user who created the workspace.
    /// </summary>
    public CommonUserResponse? CreatedByUser { get; set; }

    /// <summary>
    /// Gets or sets the role of the current user in the workspace.
    /// </summary>
    public WorkspaceRole MemberRole { get; set; }
}
