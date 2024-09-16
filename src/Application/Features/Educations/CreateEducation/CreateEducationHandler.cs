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

        var newEducation = new Education
        {
            Identifier = CustomNewId.GenerateStandardId(),
            InstitutionName = request.InstitutionName,
            Degree = request.Degree,
            FieldOfStudy = request.FieldOfStudy,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        memberProfile.Educations?.Add(newEducation);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newEducation.Adapt<EducationResponse>());
    }
}
