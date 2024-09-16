using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Platform.API.Configurations.OptionSetups;
using Platform.Application.Common;
using Platform.Application.Features.FileStorages.GenerateSasUri;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberProfiles.UpdateAvatar;

/// <summary>
/// Handles the request to update the avatar of a member.
/// </summary>
public class UpdateAvatarHandler : IRequestHandler<UpdateAvatarRequest, Result<string>>
{
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };

    private readonly IMediator mediator;
    private readonly IContext context;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAvatarHandler"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for sending requests.</param>
    /// <param name="context">The context of the current request.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage configuration options.</param>
    public UpdateAvatarHandler(IMediator mediator, IContext context, IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        this.mediator = mediator;
        this.context = context;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the request to update the avatar of a member.
    /// </summary>
    /// <param name="request">The request containing the member profile ID and the file to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<string>> Handle(UpdateAvatarRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var fileExt = Path.GetExtension(request.File.FileName)?.ToLowerInvariant();

        if (!Array.Exists(AllowedExtensions, ext => string.Equals(ext, fileExt, StringComparison.OrdinalIgnoreCase)))
            return Result.Error("Invalid file type. Only image files (.jpg, .jpeg, .png, .gif) and PDFs (.pdf) are allowed.");

        var fileName = CustomNewId.GenerateShortId() + fileExt;

        var containerClient = new BlobContainerClient(_azureBlobStorageOptions.ConnectionString, _azureBlobStorageOptions.ProfileContainer);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

        var containerBlob = containerClient.GetBlobClient(fileName);
        await using (var stream = request.File.OpenReadStream())
        {
            await containerBlob.UploadAsync(stream, cancellationToken);
        }

        memberProfile.ProfilePictureUrl = containerBlob.Uri.ToString();
        await context.SaveChangesAsync(cancellationToken);

        var result = await mediator.Send(new GenerateSasUriRequest(containerBlob.Uri.ToString()), cancellationToken);

        return Result.Success(result);
    }
}
