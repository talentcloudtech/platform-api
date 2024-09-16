using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Domain.Auth.Interfaces;

namespace Platform.Application.Features.MemberProfiles.UpdateMemberProfile;

/// <summary>
/// Handles the update member profile request.
/// </summary>
public class UpdateMemberProfileHandler(IContext context) : IRequestHandler<UpdateMemberProfileRequest, Result<MemberProfileResponse>>
{
    /// <summary>
    /// Handles the update member profile request.
    /// </summary>
    /// <param name="request">The update member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(UpdateMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var originalMemberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (originalMemberProfile is null)
            return Result.NotFound();

        var existingProfilePictureUrl = originalMemberProfile.ProfilePictureUrl;
        var existingResumeFileUrl = originalMemberProfile.ResumeFileUrl;

        originalMemberProfile.FullName = request.FullName;
        originalMemberProfile.UserName = request.UserName;
        originalMemberProfile.Subtitle = request.Subtitle;
        originalMemberProfile.ShortBio = request.ShortBio;
        originalMemberProfile.Address = request.Address;
        originalMemberProfile.Timezone = request.Timezone;
        originalMemberProfile.ExternalLinks = request.ExternalLinks;
        originalMemberProfile.Languages = request.Languages;

        context.MemberProfiles.Update(originalMemberProfile);

        await context.SaveChangesAsync(cancellationToken);
        return originalMemberProfile.Adapt<MemberProfileResponse>();
    }
}
