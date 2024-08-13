using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Platform.Domain.Entities;

namespace Platform.Application.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }

    public DbSet<Hero> Heroes { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
