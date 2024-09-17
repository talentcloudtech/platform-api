using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.ProjectInfos.DeleteProjectInfo;

/// <summary>
/// Handles the deletion of a project from a member profile.
/// </summary>
public class DeleteProjectInfoHandler(IContext context) : IRequestHandler<DeleteProjectInfoRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a project.
    /// </summary>
    /// <param name="request">The request containing the member profile ID and project ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Result indicating success or failure.</returns>
    public async Task<Result> Handle(DeleteProjectInfoRequest request, CancellationToken cancellationToken)
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

        memberProfile.ProjectInfos?.Remove(project);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
