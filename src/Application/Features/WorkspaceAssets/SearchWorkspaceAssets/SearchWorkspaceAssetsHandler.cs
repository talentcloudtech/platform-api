using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.WorkspaceAssets.SearchWorkspaceAssets;

/// <summary>
/// Handles the request to search workspace assets
/// </summary>
public class SearchWorkspaceAssetsHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchWorkspaceAssetsRequest, Result<PaginatedList<WorkspaceAssetResponse>>>
{
    /// <summary>
    /// Handles the search request
    /// </summary>
    public async Task<Result<PaginatedList<WorkspaceAssetResponse>>> Handle(SearchWorkspaceAssetsRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context.Workspaces.Include(x => x.WorkspaceMembers).FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
            return Result.NotFound("Workspace not found.");

        var isAdmin = session.Role!.Name == UserRole.Admin.Name;

        if (!isAdmin && !workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this workspace.");

        var query = context
            .WorkspaceAssets.Include(x => x.Workspace)
            .Include(x => x.Project)
            .Include(x => x.Task)
            .Where(x => x.WorkspaceId == request.WorkspaceId)
            .WhereIf(request.ProjectId != null, x => x.ProjectId == request.ProjectId)
            .WhereIf(request.TaskId != null, x => x.TaskId == request.TaskId);

        if (!string.IsNullOrEmpty(request.Search))
            query = query.Where(x => x.FileName.Contains(request.Search));

        // Apply sorting
        query =
            request.SortOrder.ToLower() == "asc"
                ? query.OrderBy(x => EF.Property<object>(x, request.SortBy))
                : query.OrderByDescending(x => EF.Property<object>(x, request.SortBy));

        var assets = await query.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<WorkspaceAssetResponse>
        {
            CurrentPage = assets.CurrentPage,
            TotalPages = assets.TotalPages,
            TotalItems = assets.TotalItems,
            Result = assets
                .Result.Select(x => new WorkspaceAssetResponse
                {
                    Id = x.Id,
                    WorkspaceId = x.WorkspaceId,
                    ProjectId = x.ProjectId,
                    FileName = x.FileName,
                    TaskId = x.TaskId,
                    FileUrl = x.FileUrl,
                    FileType = x.FileType,
                    FileSize = x.FileSize,
                    Scope = x.Scope,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    WorkspaceName = x.Workspace.Name,
                    ProjectName = x.Project?.Title,
                    TaskName = x.Task?.Title,
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
