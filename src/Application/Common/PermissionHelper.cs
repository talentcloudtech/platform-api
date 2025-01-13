using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Common;

/// <summary>
/// Helper class for handling permission-related operations.
/// </summary>
public static class PermissionHelper
{
    /// <summary>
    /// Checks if the current user has access to the task.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="taskId">The task ID.</param>
    /// <param name="currentUserId">The current user ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if the user has access, otherwise false.</returns>
    public static async Task<bool> CheckIfCurrentUserHasTaskAccessAsync(
        IContext context,
        ProjectTaskId taskId,
        UserId currentUserId,
        CancellationToken cancellationToken
    )
    {
        var task = await context
            .ProjectTasks.Include(t => t.Project.Workspace.WorkspaceMembers)
            .Include(t => t.Project.ProjectMembers)
            .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);

        if (task == null)
            return false;

        if (task.Type == TaskType.Open)
            return true;

        var hasAccess =
            task.Project.Workspace.WorkspaceMembers.Any(m => m.UserId == currentUserId) || task.Project.ProjectMembers.Any(m => m.UserId == currentUserId);

        return hasAccess;
    }
}
