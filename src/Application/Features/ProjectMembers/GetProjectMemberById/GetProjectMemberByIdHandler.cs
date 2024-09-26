using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.ProjectMembers.GetProjectMemberById;

/// <summary>
/// Handler for getting a project member by their ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The project member.</returns>
public class GetProjectMemberByIdHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetProjectMemberByIdRequest, Result<ProjectMemberResponse>>
{
    /// <summary>
    /// Handles the request to get a project member by their ID.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The project member.</returns>

    public async Task<Result<ProjectMemberResponse>> Handle(GetProjectMemberByIdRequest request, CancellationToken cancellationToken)
    {
        var project = await context
            .Projects.Include(x => x.Workspace.WorkspaceMembers)
            .Include(x => x.ProjectMembers)
            .ThenInclude(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (!project.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId) && !project.ProjectMembers.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this project.");

        var member = project.ProjectMembers.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Project member not found.");

        var response = member.Adapt<ProjectMemberResponse>();
        response.User = new CommonUserResponse(member.User, member.User.MemberProfile);
        return Result.Success(response);
    }
}
