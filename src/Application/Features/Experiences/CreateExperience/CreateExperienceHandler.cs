using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Experiences.CreateExperience;

/// <summary>
/// Handles the creation of an experience.
/// </summary>
/// <param name="session">The current session.</param>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class CreateExperienceHandler(ICurrentSession session, IContext context) : IRequestHandler<CreateExperienceRequest, Result<ExperienceResponse>>
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

        if (memberProfile.Id != session.UserId)
        {
            return Result.Forbidden();
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

        memberProfile.Experiences?.Add(newExperience);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newExperience.Adapt<ExperienceResponse>());
    }
}
