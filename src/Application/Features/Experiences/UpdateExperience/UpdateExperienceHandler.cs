using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Experiences.UpdateExperience;

/// <summary>
/// Handles the update of an experience.
/// <param name="context">The database context.</param>
/// </summary>
public class UpdateExperienceHandler(IContext context) : IRequestHandler<UpdateExperienceRequest, Result<ExperienceResponse>>
{
    /// <summary>
    /// Handles the update of an experience.
    /// </summary>
    /// <param name="request">The request containing the experience details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<ExperienceResponse>> Handle(UpdateExperienceRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound();
        }

        var experience = memberProfile.Experiences?.FirstOrDefault(e => e.Identifier == request.ExperienceId);

        if (experience == null)
        {
            return Result.NotFound();
        }

        experience.CompanyName = request.CompanyName;
        experience.JobTitle = request.JobTitle;
        experience.Industry = request.Industry;
        experience.StartDate = request.StartDate;
        experience.EndDate = request.EndDate;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(experience.Adapt<ExperienceResponse>());
    }
}
