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
public class UpdateMemberProfileHandler : IRequestHandler<UpdateMemberProfileRequest, Result<MemberProfileResponse>>
{
    private readonly ICurrentSession _session;
    private readonly IContext _context;
    private readonly IMediator _mediator;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="session">The session.</param>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage options.</param>
    public UpdateMemberProfileHandler(ICurrentSession session, IContext context, IMediator mediator, IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        _session = session;
        _context = context;
        _mediator = mediator;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the update member profile request.
    /// </summary>
    /// <param name="request">The update member profile request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update operation.</returns>
    public async Task<Result<MemberProfileResponse>> Handle(UpdateMemberProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = _session.UserId;
        var originalMemberProfile = await _context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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

        _context.MemberProfiles.Update(originalMemberProfile);

        await _context.SaveChangesAsync(cancellationToken);
        return originalMemberProfile.Adapt<MemberProfileResponse>();
    }
}
