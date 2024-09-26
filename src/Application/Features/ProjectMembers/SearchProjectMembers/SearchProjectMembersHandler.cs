using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Extensions;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.ProjectMembers.SearchProjectMembers;

/// <summary>
/// Search project members handler.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the search project members.</returns>

public class SearchProjectMembersHandler(IContext context, ICurrentSession session)
    : IRequestHandler<SearchProjectMembersRequest, Result<PaginatedList<ProjectMemberResponse>>>
{
    /// <summary>
    /// Handle the search project members request.
    /// </summary>
    /// <param name="request">The search project members request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Result<PaginatedList<ProjectMemberResponse>>> Handle(SearchProjectMembersRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.ProjectMembers)
            .Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (!project.ProjectMembers.Any(x => x.UserId == session.UserId) && !project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this project.");

        var projectMembers = context
            .ProjectMembers.Include(x => x.User.MemberProfile)
            .Where(x => x.ProjectId == request.ProjectId)
            .WhereIf(
                !string.IsNullOrEmpty(request.UserName),
                x =>
                    !string.IsNullOrEmpty(x.User.UserName)
                    && !string.IsNullOrEmpty(request.UserName)
                    && EF.Functions.Like(x.User.UserName.ToLower().Trim(), $"%{request.UserName.ToLower().Trim()}%")
            )
            .OrderBy(x => x.User.UserName);

        var projectMembersResponse = await projectMembers.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

        var paginatedResponse = new PaginatedList<ProjectMemberResponse>
        {
            CurrentPage = projectMembersResponse.CurrentPage,
            TotalPages = projectMembersResponse.TotalPages,
            TotalItems = projectMembersResponse.TotalItems,
            Result = projectMembersResponse
                .Result.Select(x =>
                {
                    var memberResponse = x.Adapt<ProjectMemberResponse>();
                    memberResponse.User = new CommonUserResponse(x.User, x.User.MemberProfile);
                    return memberResponse;
                })
                .ToList(),
        };

        return Result.Success(paginatedResponse);
    }
}
