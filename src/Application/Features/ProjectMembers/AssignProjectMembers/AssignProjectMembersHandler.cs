using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers.AssignProjectMembers;

/// <summary>
/// Handles the assignment of multiple users to a project
/// </summary>
public class AssignProjectMembersHandler : IRequestHandler<AssignProjectMembersRequest, Result>
{
    private readonly IContext _context;
    private readonly ICurrentSession _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignProjectMembersHandler"/> class
    /// </summary>
    public AssignProjectMembersHandler(IContext context, ICurrentSession session)
    {
        _context = context;
        _session = session;
    }

    /// <summary>
    /// Handles the request to assign multiple users to a project
    /// </summary>
    public async Task<Result> Handle(AssignProjectMembersRequest request, CancellationToken cancellationToken)
    {
        var project = await _context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace)
            .ThenInclude(w => w.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        var workspaceMemberIds = project.Workspace.WorkspaceMembers.Select(x => x.UserId).ToHashSet();
        var nonWorkspaceMembers = request.UserIds.Where(userId => !workspaceMemberIds.Contains(userId)).ToList();

        var currentUserRoleInWorkspaceMember = project.Workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == _session.UserId);

        // Check permissions
        if (
            _session.Role!.Name != UserRole.Admin.Name
            && (
                currentUserRoleInWorkspaceMember == null
                || (
                    currentUserRoleInWorkspaceMember.Role != WorkspaceRole.WorkspaceOwner
                    && currentUserRoleInWorkspaceMember.Role != WorkspaceRole.WorkspaceManager
                )
            )
        )
        {
            return Result.Forbidden("You don't have permission to assign members to this project.");
        }

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (_session.Role!.Name == UserRole.Admin.Name && nonWorkspaceMembers.Any())
            {
                // Admin user: First add non-workspace members to the workspace
                var newWorkspaceMembers = nonWorkspaceMembers
                    .Select(userId => new WorkspaceMember
                    {
                        WorkspaceId = request.WorkspaceId,
                        UserId = userId,
                        Role = WorkspaceRole.WorkspaceMember, // Default role for auto-added members
                        CreatedBy = _session.UserId,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedBy = _session.UserId,
                        UpdatedDate = DateTime.UtcNow,
                    })
                    .ToList();

                _context.WorkspaceMembers.AddRange(newWorkspaceMembers);
                await _context.SaveChangesAsync(cancellationToken);
            }
            // Get existing project members
            var existingMemberIds = project.ProjectMembers.Select(x => x.UserId).ToHashSet();

            // Filter out users who are already members
            var newUserIds = request.UserIds.Where(userId => !existingMemberIds.Contains(userId)).ToList();

            if (!newUserIds.Any())
                return Result.Success(); // All users are already members

            var currentTime = DateTime.UtcNow;

            // Create new project members
            var newProjectMembers = newUserIds
                .Select(userId => new ProjectMember
                {
                    ProjectId = request.ProjectId,
                    UserId = userId,
                    Role = request.Role,
                    CreatedBy = _session.UserId,
                    CreatedDate = currentTime,
                    UpdatedBy = _session.UserId,
                    UpdatedDate = currentTime,
                })
                .ToList();

            _context.ProjectMembers.AddRange(newProjectMembers);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
