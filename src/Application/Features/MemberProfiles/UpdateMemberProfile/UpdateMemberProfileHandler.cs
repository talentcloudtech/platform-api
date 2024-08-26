using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Application.Features.FileStorages.DeleteFile;
using Platform.Application.Features.FileStorages.MoveFile;

namespace Platform.Application.Features.MemberProfiles.UpdateMemberProfile;

/// <summary>
/// Handles the update member profile request.
/// </summary>
public class UpdateMemberProfileHandler : IRequestHandler<UpdateMemberProfileRequest, Result<MemberProfileResponse>>
{
    private readonly IContext _context;
    private readonly IMediator _mediator;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberProfileHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="mediator">The mediator.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage options.</param>
    public UpdateMemberProfileHandler(
        IContext context,
        IMediator mediator,
        IOptions<AzureBlobStorageOptions> azureBlobStorageOptions
    )
    {
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
            return Result.Error("Duplicate handle name found");

        string? profilePictureUrl = null;
        if (!string.IsNullOrWhiteSpace(request.ProfilePictureUrl) && UriUtils.IsSasUri(request.ProfilePictureUrl))
        {
            var moveFileResult = await _mediator.Send(
                new MoveFileRequest
                {
                    DownloadUri = request.ProfilePictureUrl,
                    TempContainerPath = _azureBlobStorageOptions.TempContainer,
                    ActualContainerPath = _azureBlobStorageOptions.ProfileContainer
                },
                cancellationToken
            );

            if (!moveFileResult.IsSuccess)
                return Result.Error("Failed to move profile picture.");

            profilePictureUrl = moveFileResult.Value;
        }

        string? resumeFileUrl = null;
        if (!string.IsNullOrWhiteSpace(request.ResumeFileUrl) && UriUtils.IsSasUri(request.ResumeFileUrl))
        {
            var moveFileResult = await _mediator.Send(
                new MoveFileRequest
                {
                    DownloadUri = request.ResumeFileUrl,
                    TempContainerPath = _azureBlobStorageOptions.TempContainer,
                    ActualContainerPath = _azureBlobStorageOptions.ResumeContainer
                },
                cancellationToken
            );

            if (!moveFileResult.IsSuccess)
                return Result.Error("Failed to move resume file.");

            resumeFileUrl = moveFileResult.Value;
        }

        var existingProfilePictureUrl = originalMemberProfile.ProfilePictureUrl;
        var existingResumeFileUrl = originalMemberProfile.ResumeFileUrl;

        originalMemberProfile.FullName = request.FullName;
        originalMemberProfile.HandleName = request.HandleName;
        originalMemberProfile.ShortBio = request.ShortBio;
        originalMemberProfile.ProfilePictureUrl = profilePictureUrl ?? request.ProfilePictureUrl;
        originalMemberProfile.ResumeFileUrl = resumeFileUrl ?? request.ResumeFileUrl;
        originalMemberProfile.Timezone = request.Timezone;
        originalMemberProfile.ExternalLinks = request.ExternalLinks;
        originalMemberProfile.Skills = request.Skills;
        originalMemberProfile.IndustryExperiences = request.IndustryExperiences;

        _context.MemberProfiles.Update(originalMemberProfile);

        await _context.SaveChangesAsync(cancellationToken);
        if (
            !string.IsNullOrWhiteSpace(existingProfilePictureUrl)
            && originalMemberProfile.ProfilePictureUrl?.ToLower().Trim() != existingProfilePictureUrl.ToLower().Trim()
        )
            await _mediator.Send(new DeleteFileRequest(existingProfilePictureUrl), cancellationToken);

        if (
            !string.IsNullOrWhiteSpace(existingResumeFileUrl)
            && originalMemberProfile.ProfilePictureUrl?.ToLower().Trim() != existingResumeFileUrl.ToLower().Trim()
        )
            await _mediator.Send(new DeleteFileRequest(existingResumeFileUrl), cancellationToken);

        return originalMemberProfile.Adapt<MemberProfileResponse>();
    }

    /// <summary>
    /// Moves a file from a temporary location to the profile container if the URL is not null or empty.
    /// </summary>
    /// <param name="fileUrl">The URL of the file to move.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="errorMessage">The error message to return if the move fails.</param>
    /// <returns>The new URL of the moved file, or null if the move fails.</returns>
    private async Task<string?> MoveFileIfNotNullAsync(
        string? fileUrl,
        CancellationToken cancellationToken,
        string errorMessage
    )
    {
        if (!string.IsNullOrEmpty(fileUrl))
        {
            var moveFileResult = await _mediator.Send(
                new MoveFileRequest
                {
                    DownloadUri = fileUrl,
                    TempContainerPath = _azureBlobStorageOptions.TempContainer,
                    ActualContainerPath = _azureBlobStorageOptions.ProfileContainer
                },
                cancellationToken
            );

            if (moveFileResult.IsSuccess)
            {
                return moveFileResult.Value;
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}
