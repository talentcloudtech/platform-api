using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.Experiences.DeleteExperience;

/// <summary>
/// Handles the deletion of an experience.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class DeleteExperienceHandler(IContext context) : IRequestHandler<DeleteExperienceRequest, Result>
{
    /// <summary>
    /// Handles the deletion of an experience.
    /// </summary>
    /// <param name="request">The request containing experience details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(DeleteExperienceRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var experience = memberProfile.Experiences?.FirstOrDefault(e => e.Identifier == request.ExperienceId);

        if (experience == null)
        {
            return Result.NotFound("Experience not found.");
        }

        memberProfile.Experiences?.Remove(experience);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
