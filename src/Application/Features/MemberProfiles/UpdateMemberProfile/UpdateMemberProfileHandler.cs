using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.UpdateMemberProfile;

/// <summary>
/// Handles the update member profile request.
/// </summary>
public class UpdateMemberProfileHandler : IRequestHandler<UpdateMemberProfileRequest, Result<MemberProfileResponse>>
{
    private readonly IContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public UpdateMemberProfileHandler(IContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the update member profile request.
    /// </summary>
    /// <param name="request">The update member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(
        UpdateMemberProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var originalMemberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(
            x => x.Id == request.Id,
            cancellationToken
        );
        if (originalMemberProfile is null)
            return Result.NotFound();

        var existHandleName = await _context
            .MemberProfiles.AnyAsync(
                x => x.Id != request.Id && x.HandleName.ToLower().Trim() == request.HandleName.ToLower().Trim(),
                cancellationToken
            )
            .ConfigureAwait(false);
        if (existHandleName)
            Result.Error("Duplicate handle name found");

        originalMemberProfile.FullName = request.FullName;
        originalMemberProfile.HandleName = request.HandleName;
        originalMemberProfile.ShortBio = request.ShortBio;
        originalMemberProfile.ProfilePictureUrl = request.ProfilePictureUrl;
        originalMemberProfile.ResumeFileUrl = request.ResumeFileUrl;
        originalMemberProfile.Timezone = request.Timezone;
        originalMemberProfile.ExternalLinks = request.ExternalLinks;
        originalMemberProfile.Skills = request.Skills;
        originalMemberProfile.IndustryExperiences = request.IndustryExperiences;

        _context.MemberProfiles.Update(originalMemberProfile);

        await _context.SaveChangesAsync(cancellationToken);

        return originalMemberProfile.Adapt<MemberProfileResponse>();
    }
}
