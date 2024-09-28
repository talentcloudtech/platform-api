using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.WorkspaceMemberInvitations.GetWorkspaceMemberInvitationById;

/// <summary>
/// Handler for getting a workspace member invitation by its ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The project member.</returns>
public class GetWorkspaceMemberInvitationByIdHandler(IContext context)
    : IRequestHandler<GetWorkspaceMemberInvitationByIdRequest, Result<WorkspaceMemberInvitationResponse>>
{
    /// <summary>
    /// Handles the request to get a workspace member invitation by its ID.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The workspace member invitation.</returns>

    public async Task<Result<WorkspaceMemberInvitationResponse>> Handle(GetWorkspaceMemberInvitationByIdRequest request, CancellationToken cancellationToken)
    {
        var workspaceMemberInvitation = await context
            .WorkspaceMemberInvitations.Include(x => x.Workspace)
            .Include(x => x.User.MemberProfile)
            .FirstOrDefaultAsync(x => x.Id == request.WorkspaceMemberInvitationId, cancellationToken);

        if (workspaceMemberInvitation == null)
            return Result.NotFound("Workspace member invitation not found.");

        var response = workspaceMemberInvitation.Adapt<WorkspaceMemberInvitationResponse>();

        response.Workspace = new WorkspaceResponse
        {
            Id = workspaceMemberInvitation.WorkspaceId,
            Name = workspaceMemberInvitation.Workspace.Name,
            ContactEmail = workspaceMemberInvitation.Workspace.ContactEmail,
            Urls = workspaceMemberInvitation.Workspace.Urls,
            Address = workspaceMemberInvitation.Workspace.Address,
            Logo = workspaceMemberInvitation.Workspace.Logo,
            CreatedDate = workspaceMemberInvitation.Workspace.CreatedDate,
        };

        response.InvitedBy = new CommonUserResponse(workspaceMemberInvitation.User, workspaceMemberInvitation.User.MemberProfile);
        return Result.Success(response);
    }
}
