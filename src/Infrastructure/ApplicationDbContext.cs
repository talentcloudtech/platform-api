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
    /// Gets or sets the users DbSet.
    /// </summary>
    public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

    /// <summary>
    /// Gets or sets the projects DbSet.
    /// </summary>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <summary>
    /// Gets or sets the Member profiles DbSet.
    /// </summary>
    public DbSet<MemberProfile> MemberProfiles { get; set; } = null!;

    // /// <summary>
    // /// Gets or sets the DbSet of ProjectUser entities.
    // /// </summary>
    // public DbSet<ProjectUser> ProjectUsers { get; set; } = null!;

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
