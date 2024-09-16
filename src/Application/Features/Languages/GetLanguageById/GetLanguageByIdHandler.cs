using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.Languages.GetLanguageById;

/// <summary>
/// Handles the request to get a language by its identifier.
/// <param name="context">The database context.</param>
/// </summary>
public class GetLanguageByIdHandler(IContext context) : IRequestHandler<GetLanguageByIdRequest, Result<LanguageResponse>>
{
    /// <summary>
    /// Handles the request to get a language by its identifier.
    /// </summary>
    /// <param name="request">The request to get a language.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The language response.</returns>
    public async Task<Result<LanguageResponse>> Handle(GetLanguageByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var language = memberProfile.Languages?.FirstOrDefault(x => x.Identifier == request.LanguageId);
        if (language == null)
        {
            return Result.NotFound("Language not found.");
        }

        return Result.Success(language.Adapt<LanguageResponse>());
    }
}
