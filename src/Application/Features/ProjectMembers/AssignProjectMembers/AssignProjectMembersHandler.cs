using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        // Check if user has permission to assign members

        var workspaceMember = project.Workspace.WorkspaceMembers.FirstOrDefault(x => x.UserId == _session.UserId);
        if (
            _session.Role!.Name != UserRole.Admin.Name
            && (workspaceMember == null || (workspaceMember.Role != WorkspaceRole.WorkspaceOwner && workspaceMember.Role != WorkspaceRole.WorkspaceManager))
        )
            return Result.Forbidden("You don't have permission to assign members to this project.");

        // Get existing project members
        var existingMemberIds = project.ProjectMembers.Select(x => x.UserId).ToHashSet();

        // Filter out users who are already members
        var newUserIds = request.UserIds.Where(userId => !existingMemberIds.Contains(userId)).ToList();

        if (!newUserIds.Any())
            return Result.Success(); // All users are already members

        // Verify all users exist and are workspace members
        var workspaceMemberIds = project.Workspace.WorkspaceMembers.Select(x => x.UserId).ToHashSet();
        // var invalidUsers = newUserIds.Where(userId => !workspaceMemberIds.Contains(userId)).ToList();

        // if (invalidUsers.Any())
        //     return Result.Error("Some users are not members of the workspace.");

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
}
