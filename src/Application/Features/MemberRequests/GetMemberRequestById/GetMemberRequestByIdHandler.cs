using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberRequests.GetMemberRequestById;

/// <summary>
/// Handles the request to retrieve a member request by its unique identifier.
/// </summary>
/// <param name="context">The database context used to access member requests.</param>
/// <returns>A <see cref="Result{T}"/> containing a <see cref="MemberRequestResponse"/> if the member request is found, or a not found result if the member request does not exist.</returns>
public class GetMemberRequestByIdHandler(IContext context) : IRequestHandler<GetMemberRequestByIdRequest, Result<MemberRequestResponse>>
{
    /// <summary>
    /// Handles the retrieval of a member request by its ID.
    /// </summary>
    /// <param name="request">The request containing the ID of the member request to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing a <see cref="MemberRequestResponse"/> if the member request is found,
    /// or a not found result if the member request does not exist.
    /// </returns>
    public async Task<Result<MemberRequestResponse>> Handle(GetMemberRequestByIdRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace.WorkspaceMembers)
            .FirstOrDefaultAsync(x => x.WorkspaceId == request.WorkspaceId && x.Id == request.Id, cancellationToken: cancellationToken);

        if (memberRequest is null)
            return Result.NotFound("Member request not found.");

        return memberRequest.Adapt<MemberRequestResponse>();
    }
}
