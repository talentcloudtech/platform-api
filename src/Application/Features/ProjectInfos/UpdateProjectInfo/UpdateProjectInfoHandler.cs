using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.ProjectInfos.UpdateProjectInfo;

/// <summary>
/// Handles the update of an existing project for a member profile.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class UpdateProjectInfoHandler(IContext context) : IRequestHandler<UpdateProjectInfoRequest, Result<ProjectInfoResponse>>
{
    /// <summary>
    /// Handles the request to update an existing project.
    /// </summary>
    /// <param name="request">The request containing the updated project details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result containing the updated ProjectInfo Response.</returns>
    public async Task<Result<ProjectInfoResponse>> Handle(UpdateProjectInfoRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.MemberProfileId, cancellationToken);
        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var project = memberProfile.ProjectInfos?.FirstOrDefault(p => p.Identifier == request.ProjectInfoId);
        if (project == null)
        {
            return Result.NotFound("Project info not found.");
        }

        project.Title = request.Title;
        project.Link = request.Link;
        project.ShortDescription = request.ShortDescription;
        project.Technologies = request.Technologies;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(project.Adapt<ProjectInfoResponse>());
    }
}
