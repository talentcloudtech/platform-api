using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Projects.GetUserProjectRole;

/// <summary>
/// Handler for getting the role of a user in a project.
/// </summary>
/// <param name="context">The database context. </param>
/// <param name="session">The current session. </param>
public class GetUserProjectRoleHandler(IContext context, ICurrentSession session)
    : IRequestHandler<GetUserProjectRoleRequest, Result<ProjectMemberRoleResponse>>
{
    /// <summary>
    /// Handles the request to get the role of a user in a project.
    /// </summary>
    /// <param name="request">The request to get the role of a user in a project.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The role of the user in the project. </returns>
    public async Task<Result<ProjectMemberRoleResponse>> Handle(GetUserProjectRoleRequest request, CancellationToken cancellationToken)
    {
        var projectMemberRole = await context.ProjectMembers.FirstOrDefaultAsync(
            pm => pm.ProjectId == request.ProjectId && pm.Project.WorkspaceId == request.WorkspaceId && pm.UserId == session.UserId,
            cancellationToken
        );
        if (projectMemberRole == null)
            return Result.NotFound("Project member role not found.");

        return Result.Success(new ProjectMemberRoleResponse { Role = projectMemberRole.Role });
    }
}
