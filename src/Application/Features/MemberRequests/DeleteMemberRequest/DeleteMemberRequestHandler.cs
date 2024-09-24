using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.MemberRequests.DeleteMemberRequest;

/// <summary>
/// Handles the request to delete a member request.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="session">The current session.</param>
/// <returns>The result of the delete operation.</returns>
public class DeleteMemberRequestHandler(IContext context, ICurrentSession session) : IRequestHandler<DeleteMemberRequestRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a member request.
    /// </summary>
    /// <param name="request">The delete member request request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<Result> Handle(DeleteMemberRequestRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.WorkspaceId == request.WorkspaceId && x.Id == request.Id, cancellationToken);
        if (memberRequest is null)
            return Result.NotFound("Member request not found.");

        if (!memberRequest.Workspace.WorkspaceMembers.Any(x => x.UserId == session.UserId && x.Role == WorkspaceRole.WorkspaceOwner))
            return Result.Forbidden("You don't have permission to delete this member request.");

        context.MemberRequests.Remove(memberRequest);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
