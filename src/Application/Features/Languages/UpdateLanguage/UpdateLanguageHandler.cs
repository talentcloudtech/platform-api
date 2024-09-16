using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.Languages.UpdateLanguage;

namespace Platform.Application.Features.Languages.UpdateLanguage;

/// <summary>
/// Handles the request to update a language.
/// </summary>
/// <param name="context">The database context.</param>
public class UpdateLanguageHandler(IContext context) : IRequestHandler<UpdateLanguageRequest, Result<LanguageResponse>>
{
    /// <summary>
    /// Handles the request to update a language.
    /// </summary>
    /// <param name="request">The request containing the language details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<LanguageResponse>> Handle(UpdateLanguageRequest request, CancellationToken cancellationToken)
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

        language.Name = request.Name;
        language.ProficiencyLevel = request.ProficiencyLevel;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(language.Adapt<LanguageResponse>());
    }
}
