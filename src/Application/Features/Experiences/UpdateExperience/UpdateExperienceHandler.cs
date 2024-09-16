using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Auth;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Infrastructure;

namespace Platform.Application.Features.Experiences.UpdateExperience;

/// <summary>
/// Handles the update of an experience.
/// <param name="session">The current session.</param>
/// <param name="context">The database context.</param>
/// </summary>
public class UpdateExperienceHandler(ICurrentSession session, IContext context) : IRequestHandler<UpdateExperienceRequest, Result<ExperienceResponse>>
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

        if (memberProfile.Id != session.UserId)
        {
            return Result.Forbidden();
        }

        var experience = memberProfile.Experiences.FirstOrDefault(e => e.Identifier == request.ExperienceId);

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
