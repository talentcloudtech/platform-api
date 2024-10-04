using System;
using System.IO;
using System.Linq;
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
using Platform.Application.Features.FileStorages.DeleteFile;
using Platform.Domain.Entities.Common;
using Platform.Domain.Entities.Enums;

namespace Platform.Application.Features.Workspaces.UploadWorkspaceAvatar;

/// <summary>
/// Handles the upload workspace avatar request.
/// </summary>
public class UploadWorkspaceAvatarHandler : IRequestHandler<UploadWorkspaceAvatarRequest, Result<UploadWorkspaceAvatarResponse>>
{
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
    private readonly IMediator mediator;
    private readonly IContext context;
    private readonly AzureBlobStorageOptions _azureBlobStorageOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="UploadWorkspaceAvatarHandler"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for sending requests.</param>
    /// <param name="context">The context of the current request.</param>
    /// <param name="azureBlobStorageOptions">The Azure Blob Storage configuration options.</param>
    public UploadWorkspaceAvatarHandler(IMediator mediator, IContext context, IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        this.mediator = mediator;
        this.context = context;
        _azureBlobStorageOptions = azureBlobStorageOptions.Value;
    }

    /// <summary>
    /// Handles the upload workspace avatar request.
    /// </summary>
    /// <param name="request">The upload workspace avatar request.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the URL of the uploaded file in the actual container.
    /// </returns>
    public async Task<Result<UploadWorkspaceAvatarResponse>> Handle(UploadWorkspaceAvatarRequest request, CancellationToken cancellationToken)
    {
        var workspace = await context
            .Workspaces.Include(x => x.WorkspaceMembers)
            .Where(x =>
                x.WorkspaceMembers.Any(y =>
                    y.UserId == request.CurrentUserId && (y.Role == WorkspaceRole.WorkspaceOwner || y.Role == WorkspaceRole.WorkspaceManager)
                )
            )
            .FirstOrDefaultAsync(x => x.Id == request.WorkspaceId, cancellationToken);

        if (workspace == null)
        {
            return Result.Forbidden("You can not update the workspace or doesn't exist.");
        }

        var fileExt = Path.GetExtension(request.File.FileName)?.ToLowerInvariant();

        if (!Array.Exists(AllowedExtensions, ext => string.Equals(ext, fileExt, StringComparison.OrdinalIgnoreCase)))
            return Result.Error("Invalid file type. Only image files (.jpg, .jpeg, .png, .gif, .pdf) are allowed.");

        var fileName = CustomNewId.GenerateShortId() + fileExt;

        var containerClient = new BlobContainerClient(_azureBlobStorageOptions.ConnectionString, "workspace-logos");

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var containerBlob = containerClient.GetBlobClient(fileName);


        await using (var stream = request.File.OpenReadStream())
        {
            await containerBlob.UploadAsync(stream, cancellationToken);
        }

        containerBlob.SetHttpHeaders(new BlobHttpHeaders
        {
            CacheControl = "public, max-age=31536000, immutable",
            ContentType = FileHelper.GetContentType(fileName),
        });

        var existingFileUrl = workspace.Logo;

        var newProfilePictureUrl = containerBlob.Uri.ToString();

        workspace.Logo = newProfilePictureUrl;

        await context.SaveChangesAsync(cancellationToken);

        if (existingFileUrl != null)
        {
            var result = await mediator.Send(new DeleteFileRequest(existingFileUrl), cancellationToken);
        }

        return Result.Success(new UploadWorkspaceAvatarResponse { FileUrl = newProfilePictureUrl });
    }
}
