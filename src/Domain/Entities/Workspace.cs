using System;
using System.Collections.Generic;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents a workspace entity in the system.
/// </summary>

public class Workspace : AuditableEntity<WorkspaceId>
{
    /// <summary>
    /// Gets or sets the unique identifier for the workspace.
    /// </summary>
    public override WorkspaceId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the name of the workspace.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the contact email for the workspace.
    /// </summary>
    public string? ContactEmail { get; set; }

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
    /// Gets or sets the current balance of the workspace.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the workspace is the default workspace.
    /// </summary>
    public bool IsDefault { get; set; } = false;

    /// <summary>
    /// Gets or sets the user associated with this project.
    /// </summary>
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the members of the workspace.
    /// </summary>
    public ICollection<WorkspaceMember> WorkspaceMembers { get; set; } = new List<WorkspaceMember>();

    /// <summary>
    /// Gets or sets the invitations to join the workspace.
    /// </summary>
    public ICollection<WorkspaceMemberInvitation> WorkspaceMemberInvitations { get; set; } = new List<WorkspaceMemberInvitation>();

    /// <summary>
    /// Gets or sets the collection of projects.
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();

    /// <summary>
    /// Gets or sets the collection of tasks.
    /// </summary>
    public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();

    /// <summary>
    /// Gets or sets the collection of contracts.
    /// </summary>
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    /// <summary>
    /// Gets or sets the collection of member requests.
    /// </summary>
    public ICollection<MemberRequest> MemberRequests { get; set; } = new List<MemberRequest>();

    /// <summary>
    /// Gets or sets the transactions of the workspace.
    /// </summary>
    public ICollection<WorkspaceTransaction> WorkspaceTransactions { get; set; } = new List<WorkspaceTransaction>();
}
