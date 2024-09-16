using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Experiences.GetExperienceById;

/// <summary>
/// Handles the retrieval of an experience by its ID.
/// <param name="context">The database context.</param>
/// </summary>
public class GetExperienceByIdHandler(IContext context) : IRequestHandler<GetExperienceByIdRequest, Result<ExperienceResponse>>
{
    /// <summary>
    /// Handles the retrieval of an experience by its ID.
    /// </summary>
    /// <param name="request">The request containing experience details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<ExperienceResponse>> Handle(GetExperienceByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }
        var experience = memberProfile.Experiences?.FirstOrDefault(x => x.Identifier == request.ExperienceId);
        if (experience == null)
        {
            return Result.NotFound("Experience not found.");
        }

        return Result.Success(experience.Adapt<ExperienceResponse>());
    }
}
