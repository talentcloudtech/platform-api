using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Educations.DeleteEducation;

/// <summary>
/// Handles the request to delete an education.
/// </summary>
/// <param name="context">The database context.</param>
public class DeleteEducationHandler(IContext context) : IRequestHandler<DeleteEducationRequest, Result>
{
    /// <summary>
    /// Handles the request to delete an education.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(DeleteEducationRequest request, CancellationToken cancellationToken)
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

        memberProfile.Educations.Remove(education);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
