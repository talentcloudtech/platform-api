using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Platform.Domain.Entities.Common;

namespace Platform.Domain.Entities;

/// <summary>
/// Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser<UserId>
{
    /// <summary>
    /// Gets or sets the ID of the application user.
    /// </summary>
    public override UserId Id { get; set; } = CustomNewId.GenerateStandardId();

    /// <summary>
    /// Gets or sets the member profile associated with the user.
    /// </summary>
    public MemberProfile? MemberProfile { get; set; }

    /// <summary>
    /// Gets or sets the refresh tokens associated with the user.
    /// </summary>
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    /// <summary>
    /// Gets or sets the workspaces associated with the user.
    /// </summary>
    public List<Workspace> Workspaces { get; set; } = [];

    /// <summary>
    /// Gets or sets the workspace members associated with the user.
    /// </summary>
    public List<WorkspaceMember> WorkspaceMembers { get; set; } = [];

    /// <summary>
    /// Gets or sets the projects associated with the user.
    /// </summary>
    public List<Project> Projects { get; set; } = [];

    /// <summary>
    /// Gets or sets the project members associated with the user.
    /// </summary>
    public List<ProjectMember> ProjectMembers { get; set; } = [];

    /// <summary>
    /// Gets or sets the tasks associated with the user.
    /// </summary>
    public List<ProjectTask> Tasks { get; set; } = [];

    /// <summary>
    /// Gets or sets the tasks associated with the user.
    /// </summary>
    public List<ProjectTaskAssignment> TaskAssignments { get; set; } = [];

    /// <summary>
    /// Gets or sets the contracts associated with the user.
    /// </summary>
    public List<Contract> Contracts { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of member requests.
    /// </summary>
    public List<MemberRequest> MemberRequests { get; set; } = [];
}

/// <summary>
/// Represents an application role.
/// </summary>
public class ApplicationRole : IdentityRole<UserId>
{
    /// <summary>
    /// Gets or sets the ID of the application role.
    /// </summary>
    public override UserId Id { get; set; } = CustomNewId.GenerateStandardId();
}
