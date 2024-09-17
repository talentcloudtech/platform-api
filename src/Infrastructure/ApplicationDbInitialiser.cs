using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Platform.Infrastructure.DataSeed;

namespace Platform.Infrastructure;

/// <summary>
/// Initializes the application database.
/// </summary>
public class ApplicationDbInitializer : IHostedService
{
    private readonly ILogger<ApplicationDbInitializer> _logger;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbInitializer"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public ApplicationDbInitializer(ILogger<ApplicationDbInitializer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Starts the application asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var strategy = context.Database.CreateExecutionStrategy();

        _logger.LogInformation("Running migrations for {Context}", nameof(ApplicationDbContext));

        await strategy.ExecuteAsync(async () => await context.Database.MigrateAsync(cancellationToken: cancellationToken));
        await SeedDataAsync(context, cancellationToken);

        _logger.LogInformation("Migrations applied successfully");
    }

    /// <summary>
    /// Stops the application asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <summary>
    /// Seeds the default data into the database.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task SeedDataAsync(ApplicationDbContext context, CancellationToken cancellationToken)
    {
        await RoleData.SeedAsync(context, cancellationToken);
        await SkillData.SeedAsync(context, cancellationToken);
    }
}
