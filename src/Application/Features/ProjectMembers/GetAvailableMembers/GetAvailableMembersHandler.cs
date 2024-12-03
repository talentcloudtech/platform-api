using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.ProjectMembers.GetAvailableMembers;

/// <summary>
/// Handles getting available members that can be assigned to a project
/// </summary>
public class GetAvailableMembersHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetAvailableMembersRequest, Result<PaginatedList<CommonUserResponse>>>
{
    /// <inheritdoc/>
    /// <param name="request">The request to get available members.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<PaginatedList<CommonUserResponse>>> Handle(
        GetAvailableMembersRequest request,
        CancellationToken cancellationToken)
    {
        // Verify project exists and user has access
        var project = await context.Projects
            .Include(p => p.ProjectMembers)
            .Include(p => p.Workspace)
                .ThenInclude(w => w.WorkspaceMembers)
            .FirstOrDefaultAsync(p =>
                p.Id == request.ProjectId &&
                p.WorkspaceId == request.WorkspaceId,
                cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        // Check if user has permission
        var hasAccess = session.Role!.Name == UserRole.Admin.Name;

        if (!hasAccess)
            return Result.Forbidden("You don't have permission to view available members.");

        // Get existing project member IDs
        var existingMemberIds = project.ProjectMembers
            .Select(pm => pm.UserId)
            .ToList();

        // Get workspace members who are not project members
        var query = context.Users
            .Include(u => u.MemberProfile)
            .Where(u => !existingMemberIds.Contains(u.Id));

        // Apply search filter
        if (!string.IsNullOrEmpty(request.Search))
        {
            var searchTerm = request.Search.ToLower().Trim();
            query = query.Where(u =>
                EF.Functions.Like(u.UserName!.ToLower(), $"%{searchTerm}%") ||
                EF.Functions.Like(u.Email!.ToLower(), $"%{searchTerm}%") ||
                (u.MemberProfile != null &&
                 EF.Functions.Like(u.MemberProfile.FullName!.ToLower(), $"%{searchTerm}%")));
        }

        // Get paginated results
        var members = await query
            .OrderBy(u => u.UserName)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var response = new PaginatedList<CommonUserResponse>
        {
            CurrentPage = members.CurrentPage,
            TotalPages = members.TotalPages,
            TotalItems = members.TotalItems,
            Result = members.Result
                .Select(u => new CommonUserResponse(u, u.MemberProfile))
                .ToList()
        };

        return Result.Success(response);
    }
}