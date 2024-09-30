using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.MemberProfiles;

namespace Platform.Application.Features.Languages.DeleteLanguage;

/// <summary>
/// Handles the request to delete a language.
/// </summary>
/// <param name="context">The database context.</param>
public class DeleteLanguageHandler(IContext context) : IRequestHandler<DeleteLanguageRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a language.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Result> Handle(DeleteLanguageRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var language = memberProfile.Languages?.FirstOrDefault(e => e.Identifier == request.LanguageId);

        if (language == null)
        {
            return Result.NotFound("Language not found.");
        }

        memberProfile.Languages?.Remove(language);
        memberProfile.IsProfileComplete = ProfileScoreCalculator.IsProfileComplete(memberProfile);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
