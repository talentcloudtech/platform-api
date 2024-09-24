using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Platform.Domain.Entities;

namespace Platform.Application.Common;

/// <summary>
/// Represents the context interface.
/// </summary>
public interface IContext : IDataProtectionKeyContext, IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets the database facade.
    /// </summary>
    public DatabaseFacade Database { get; }

    /// <summary>
    /// Gets or sets the refresh tokens DbSet.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; }

    /// <summary>
    /// Gets or sets the users DbSet.
    /// </summary>
    public DbSet<ApplicationUser> Users { get; }

    /// <summary>
    /// Gets or sets the member profile DbSet.
    /// </summary>
    public DbSet<MemberProfile> MemberProfiles { get; }

    /// <summary>
    /// Gets or sets the skill DbSet.
    /// </summary>
    public DbSet<Skill> Skills { get; }

    /// <summary>
    /// Gets or sets the DbSet of Workspace entities.
    /// </summary>
    public DbSet<Workspace> Workspaces { get; }

    /// <summary>
    /// Gets or sets the DbSet of WorkspaceMember entities.
    /// </summary>
    public DbSet<WorkspaceMember> WorkspaceMembers { get; }

    /// <summary>
    /// Gets or sets the DbSet of Contract entities.
    /// </summary>
    public DbSet<Contract> Contracts { get; }

    /// <summary>
    /// Gets or sets the DbSet of Project entities.
    /// </summary>
    public DbSet<Project> Projects { get; }

    /// <summary>
    /// Gets or sets the DbSet of ProjectMember entities.
    /// </summary>
    public DbSet<ProjectMember> ProjectMembers { get; }

    /// <summary>
    /// Gets or sets the DbSet of Task entities.
    /// </summary>
    public DbSet<ProjectTask> ProjectTasks { get; }

    /// <summary>
    /// Gets or sets the DbSet of TaskAssignment entities.
    /// </summary>
    public DbSet<ProjectTaskAssignment> ProjectTaskAssignments { get; }

    /// <summary>
    /// Gets or sets the DbSet of Message entities.
    /// </summary>
    public DbSet<Message> Messages { get; }

    /// <summary>
    /// Gets or sets the DbSet of MessageRecipient entities.
    /// </summary>
    public DbSet<MessageRecipient> MessageRecipients { get; }

    /// <summary>
    /// Gets or sets the DbSet of MessageReply entities.
    /// </summary>
    public DbSet<MessageReply> MessageReplies { get; }

    /// <summary>
    /// Gets or sets the DbSet of MessageAttachment entities.
    /// </summary>
    public DbSet<MessageAttachment> MessageAttachments { get; }

    /// <summary>
    /// Gets or sets the DbSet of MemberRequest entities.
    /// </summary>
    public DbSet<MemberRequest> MemberRequests { get; }

    /// <summary>
    /// Gets or sets the DbSet of MemberRequestInvitation entities.
    /// </summary>
    public DbSet<MemberRequestInvitation> MemberRequestInvitations { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
