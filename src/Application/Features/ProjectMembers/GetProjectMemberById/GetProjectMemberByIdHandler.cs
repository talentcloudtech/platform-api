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
            .Projects.Include(x => x.Workspace.Members)
            .Include(x => x.Members)
            .ThenInclude(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId && x.WorkspaceId == request.WorkspaceId, cancellationToken);

        if (project == null)
            return Result.NotFound("Project not found.");

        if (!project.Workspace.Members.Any(x => x.UserId == session.UserId) && !project.Members.Any(x => x.UserId == session.UserId))
            return Result.Forbidden("You are not a member of this project.");

        var member = project.Members.FirstOrDefault(x => x.UserId == request.UserId);
        if (member == null)
            return Result.NotFound("Project member not found.");

        var response = member.Adapt<ProjectMemberResponse>();
        var nameParts = member.User?.MemberProfile?.FullName?.Split(' ');
        response.User = new CommonUserResponse
        {
            UserId = member.UserId,
            UserName = member.User?.UserName,
            Email = member.User?.Email,
            FirstName = nameParts?.Length > 0 ? nameParts[0] : null,
            LastName = nameParts?.Length > 1 ? nameParts[1] : null,
            ProfilePictureUrl = member.User?.MemberProfile?.ProfilePictureUrl,
        };

        return Result.Success(response);
    }
}
