using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Infrastructure.Configuration;

namespace Platform.Infrastructure;

/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, UserId>, IContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <summary>
    /// Gets or sets the refresh tokens DbSet.
    /// </summary>
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    /// <summary>
    /// Gets or sets the users DbSet.
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the skills DbSet.
    /// </summary>
    public DbSet<Skill> Skills { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Member profiles DbSet.
    /// </summary>
    public DbSet<MemberProfile> MemberProfiles { get; set; } = null!;

    /// <summary>
    /// Gets or sets the data protection keys DbSet.
    /// </summary>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of Workspace entities.
    /// </summary>
    public DbSet<Workspace> Workspaces { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of WorkspaceMember entities.
    /// </summary>
    public DbSet<WorkspaceMember> WorkspaceMembers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of WorkspaceMemberInvitation entities.
    /// </summary>
    public DbSet<WorkspaceMemberInvitation> WorkspaceMemberInvitations { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of Contract entities.
    /// </summary>
    public DbSet<Contract> Contracts { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of Project entities.
    /// </summary>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of ProjectMember entities.
    /// </summary>
    public DbSet<ProjectMember> ProjectMembers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of Task entities.
    /// </summary>
    public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of TaskAssignment entities.
    /// </summary>
    public DbSet<ProjectTaskAssignment> ProjectTaskAssignments { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of Message entities.
    /// </summary>
    public DbSet<Message> Messages { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of MessageRecipient entities.
    /// </summary>
    public DbSet<MessageRecipient> MessageRecipients { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of MessageReply entities.
    /// </summary>
    public DbSet<MessageReply> MessageReplies { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of MessageAttachment entities.
    /// </summary>
    public DbSet<MessageAttachment> MessageAttachments { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of MemberRequest entities.
    /// </summary>
    public DbSet<MemberRequest> MemberRequests { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of GigApplicantInvitation entities.
    /// </summary>
    public DbSet<GigApplicantInvitation> GigApplicantInvitations { get; set; } = null!;

    /// <summary>
    /// Gets or sets the DbSet of RemoteJob entities.
    /// </summary>
    public DbSet<RemoteJob> RemoteJobs { get; set; } = null!;

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="builder">The builder to use for configuration.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasOne(a => a.MemberProfile).WithOne(p => p.User).HasForeignKey<MemberProfile>(p => p.Id);
        });
    }
}
