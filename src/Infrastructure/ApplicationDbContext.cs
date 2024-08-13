using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Infrastructure.Configuration;

namespace Platform.Infrastructure;

/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <summary>
    /// Gets or sets the projects DbSet.
    /// </summary>
    public DbSet<Project> Projects { get; set; } = null!;

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
        // builder.ApplyConfigurationsFromAssembly(typeof(ProjectUserConfiguration).Assembly);
        // builder
        //     .Entity<ProjectUser>()
        //     .HasOne(p => p.Project)
        //     .WithMany(b => b.ProjectUsers)
        //     .HasForeignKey(bc => bc.ProjectId);

        // builder.Entity<ProjectUser>().HasOne(p => p.User).WithMany(b => b.ProjectUsers).HasForeignKey(bc => bc.UserId);
    }
}
