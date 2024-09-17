using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.ProjectInfos.GetProjectInfoById;

/// <summary>
/// Handles the retrieval of a project by its ID for a specific member profile.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class GetProjectInfoByIdHandler(IContext context) : IRequestHandler<GetProjectInfoByIdRequest, Result<ProjectInfoResponse>>
{
    /// <summary>
    /// Handles the request to retrieve a project by its ID.
    /// </summary>
    /// <param name="request">The request containing the member profile ID and project ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the ProjectResponse if found, or an error if not found.</returns>
    public async Task<Result<ProjectInfoResponse>> Handle(GetProjectInfoByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(p => p.Id == request.MemberProfileId, cancellationToken);
        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var project = memberProfile.ProjectInfos?.FirstOrDefault(p => p.Identifier == request.ProjectInfoId);
        if (project == null)
        {
            return Result.NotFound("Project info not found.");
        }

        return Result.Success(project.Adapt<ProjectInfoResponse>());
    }
}
