using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Platform.Domain.Entities;

namespace Platform.Application.Common;

/// <summary>
/// Represents the context interface.
/// </summary>
public interface IContext : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets the database facade.
    /// </summary>
    public DatabaseFacade Database { get; }

    /// <summary>
    /// Gets or sets the users DbSet.
    /// </summary>
    public DbSet<ApplicationUser> Users { get; }

    /// <summary>
    /// Gets or sets the projects DbSet.
    /// </summary>
    public DbSet<Project> Projects { get; }

    /// <summary>
    /// Gets or sets the member profile DbSet.
    /// </summary>
    public DbSet<MemberProfile> MemberProfiles { get; }

    // /// <summary>
    // /// Gets or sets the DbSet of ProjectUser entities.
    // /// </summary>
    // public DbSet<ProjectUser> ProjectUsers { get; }

    /// <summary>
    /// Asynchronously saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
