using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Platform.Application.Common;

/// <summary>
/// Helper class for handling permission-related operations.
/// </summary>
public static class PermissionHelper
{
    /// <summary>
    /// Gets a project with its members asynchronously.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="projectId">The project ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The project.</returns>
    public static async Task<Project?> GetProjectWithMembersAsync(IContext context, ProjectId projectId, CancellationToken cancellationToken)
    {
        return await context.Projects
            .Include(p => p.Workspace.WorkspaceMembers)
            .Include(p => p.ProjectMembers)
            .FirstOrDefaultAsync(p => p.Id == projectId, cancellationToken);
    }
}
