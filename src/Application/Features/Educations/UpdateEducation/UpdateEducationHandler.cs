using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Educations.UpdateEducation;

/// <summary>
/// Handles the request to update an education.
/// </summary>
/// <param name="context">The database context.</param>
public class UpdateEducationHandler(IContext context) : IRequestHandler<UpdateEducationRequest, Result<EducationResponse>>
{
    /// <summary>
    /// Handles the request to update an education.
    /// </summary>
    /// <param name="request">The request containing the education details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<EducationResponse>> Handle(UpdateEducationRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }
        var education = memberProfile.Educations.FirstOrDefault(e => e.Identifier == request.EducationId);

        if (education == null)
        {
            return Result.NotFound("Education not found.");
        }

        education.InstitutionName = request.InstitutionName;
        education.Degree = request.Degree;
        education.FieldOfStudy = request.FieldOfStudy;
        education.StartDate = request.StartDate;
        education.EndDate = request.EndDate;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(education.Adapt<EducationResponse>());
    }
}
