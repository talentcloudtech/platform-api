using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Experiences.CreateExperience;

/// <summary>
/// Handles the creation of an experience.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class CreateExperienceHandler(IContext context) : IRequestHandler<CreateExperienceRequest, Result<ExperienceResponse>>
{
    /// <summary>
    /// Handles the creation of an experience.
    /// </summary>
    /// <param name="request">The request containing experience details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<ExperienceResponse>> Handle(CreateExperienceRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var newExperience = new Experience
        {
            Identifier = CustomNewId.GenerateStandardId(),
            CompanyName = request.CompanyName,
            JobTitle = request.JobTitle,
            Industry = request.Industry,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
        if (memberProfile.Experiences == null)
            memberProfile.Experiences = new List<Experience>();

        memberProfile.Experiences?.Add(newExperience);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newExperience.Adapt<ExperienceResponse>());
    }
}
