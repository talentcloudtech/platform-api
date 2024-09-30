using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

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
        MemberProfile? memberProfile = null;
        memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var currentUser = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (memberProfile is null)
        {
            memberProfile = new MemberProfile
            {
                Id = request.Id,
                UserName = currentUser?.UserName,
                FullName = request.FullName,
                Subtitle = request.Subtitle,
                ShortBio = request.ShortBio,
                Address = request.Address,
                Timezone = request.Timezone,
                ExternalLinks = request.ExternalLinks,
            };
            memberProfile.IsProfileComplete = ProfileScoreCalculator.IsProfileComplete(memberProfile);
            context.MemberProfiles.Add(memberProfile);
        }
        else
        {
            memberProfile.FullName = request.FullName;
            memberProfile.Subtitle = request.Subtitle;
            memberProfile.ShortBio = request.ShortBio;
            memberProfile.Address = request.Address;
            memberProfile.Timezone = request.Timezone;
            memberProfile.ExternalLinks = request.ExternalLinks;
            memberProfile.IsProfileComplete = ProfileScoreCalculator.IsProfileComplete(memberProfile);
            context.MemberProfiles.Update(memberProfile);
        }

        await context.SaveChangesAsync(cancellationToken);
        return memberProfile.Adapt<MemberProfileResponse>();
    }
}
