using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Common.Responses;
using Platform.Application.Features.MemberRequests;
using Platform.Application.Features.Workspaces;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.GigInvitations.GetPublicGigOpportunityById;

/// <summary>
/// Handler for getting a gig opportunity by id
/// </summary>
/// <param name="context">The database context</param>
public class GetPublicGigOpportunityByIdHandler(IContext context) : IRequestHandler<GetPublicGigOpportunityByIdRequest, Result<MemberRequestResponse>>
{
    /// <summary>
    /// Handles the request to get a gig opportunity by id
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>The gig opportunity</returns>
    public async Task<Result<MemberRequestResponse>> Handle(GetPublicGigOpportunityByIdRequest request, CancellationToken cancellationToken)
    {
        var memberRequest = await context
            .MemberRequests.Include(x => x.Workspace)
            .Include(x => x.User.MemberProfile)
            .Where(x => x.Visibility == MemberRequestVisibility.Public)
            .FirstOrDefaultAsync(x => x.Id == request.memberRequestId, cancellationToken);

        if (memberRequest == null)
            return Result.NotFound("Member request not found");

        var response = memberRequest.Adapt<MemberRequestResponse>();
        response.Workspace = memberRequest.Workspace.Adapt<WorkspaceResponse>();
        response.CreatedByUser = new CommonUserResponse(memberRequest.User, memberRequest.User.MemberProfile);
        return response;
    }
}
