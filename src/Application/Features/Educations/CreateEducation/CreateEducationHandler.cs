using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.Educations.CreateEducation;

/// <summary>
/// Handles the request to create an education.
/// </summary>
/// <param name="context">The database context.</param>
public class CreateEducationHandler(IContext context) : IRequestHandler<CreateEducationRequest, Result<EducationResponse>>
{
    /// <summary>
    /// Handles the request to create an education.
    /// </summary>
    /// <param name="request">The request containing the education details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<EducationResponse>> Handle(CreateEducationRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var existEducation = memberProfile.Educations?.FirstOrDefault(predicate: x =>
            x.InstitutionName?.ToLower().Trim() == request.InstitutionName?.ToLower().Trim() && x.Degree?.ToLower().Trim() == request.Degree?.ToLower().Trim()
        );
        if (existEducation != null)
        {
            return Result.Conflict("Duplicate education found");
        }

        var newEducation = new Education
        {
            Identifier = CustomNewId.GenerateStandardId(),
            InstitutionName = request.InstitutionName,
            Degree = request.Degree,
            FieldOfStudy = request.FieldOfStudy,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        if (memberProfile.Educations == null)
            memberProfile.Educations = new List<Education>();

        memberProfile.Educations?.Add(newEducation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newEducation.Adapt<EducationResponse>());
    }
}
