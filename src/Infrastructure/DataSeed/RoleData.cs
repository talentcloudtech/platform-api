using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Infrastructure.DataSeed;

/// <summary>
/// Role data seed.
/// </summary>
public static class RoleData
{
    /// <summary>
    /// Seeds the role data.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The task completed.</returns>
    public static async Task SeedAsync(ApplicationDbContext context, ILogger logger, CancellationToken cancellationToken)
    {
        if (!await context.Roles.AnyAsync(cancellationToken))
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = UserRole.Admin.Name, NormalizedName = UserRole.Admin.Name.ToUpperInvariant() },
                new ApplicationRole { Name = UserRole.User.Name, NormalizedName = UserRole.User.Name.ToUpperInvariant() },
                new ApplicationRole { Name = UserRole.Customer.Name, NormalizedName = UserRole.Customer.Name.ToUpperInvariant() },
            };
            await context.Roles.AddRangeAsync(roles, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Role data seeding completed successfully.");
        }
    }
}
