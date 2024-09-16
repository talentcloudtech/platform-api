using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Educations.GetEducationById;

/// <summary>
/// Handles the request to get an education by its identifier.
/// </summary>
/// <param name="context">The database context. </param>
public class GetEducationByIdHandler(IContext context) : IRequestHandler<GetEducationByIdRequest, Result<EducationResponse>>
{
    /// <summary>
    /// Handles the request to get an education by its identifier.
    /// </summary>
    /// <param name="request">The request containing the education identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<EducationResponse>> Handle(GetEducationByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var education = memberProfile.Educations.FirstOrDefault(x => x.Identifier == request.EducationId);
        if (education == null)
        {
            return Result.NotFound("Education not found.");
        }

        return Result.Success(education.Adapt<EducationResponse>());
    }
}
