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

namespace Platform.Application.Features.Languages.CreateLanguage;

/// <summary>
/// Handles the request to create a new language entry.
/// <pram name="context">The database context.</pram>
/// </summary>
public class CreateLanguageHandler(IContext context) : IRequestHandler<CreateLanguageRequest, Result<LanguageResponse>>
{
    /// <summary>
    /// Handles the request to create a new language entry.
    /// </summary>
    /// <param name="request">The request to create a language.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created language response.</returns>
    public async Task<Result<LanguageResponse>> Handle(CreateLanguageRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var existLanguage = memberProfile.Languages?.FirstOrDefault(x => x.Name?.ToLower().Trim() == request.Name?.ToLower().Trim());
        if (existLanguage != null)
        {
            return Result.Conflict("Duplicate language found");
        }

        var newLanguage = new Language
        {
            Identifier = CustomNewId.GenerateStandardId(),
            Name = request.Name,
            ProficiencyLevel = request.ProficiencyLevel,
        };

        if (memberProfile.Languages == null)
            memberProfile.Languages = new List<Language>();

        memberProfile.Languages?.Add(newLanguage);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(newLanguage.Adapt<LanguageResponse>());
    }
}
