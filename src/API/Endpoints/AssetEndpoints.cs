using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.FileStorages.UploadFileLink;
using Platform.Application.Features.FileStorages.UploadWorkspaceFile;
using Platform.Application.Features.WorkspaceAssets.DeleteWorkspaceAsset;
using Platform.Application.Features.WorkspaceAssets.DownloadAsset;
using Platform.Application.Features.WorkspaceAssets.SearchWorkspaceAssets;
using Platform.Application.Features.WorkspaceAssets.UpdateWorkspaceAsset;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for file storage.
/// </summary>
public class AssetEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for file storage.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/assets/{workspaceId}").WithTags("Assets").RequireAuthorization();

        group
            .MapPost(
                "/",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId? projectId, ProjectTaskId? taskId, IFormFile file) =>
                {
                    var request = new UploadWorkspaceFileRequest
                    {
                        WorkspaceId = workspaceId,
                        ProjectId = projectId,
                        TaskId = taskId,
                        File = file,
                    };
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Upload file and generate download url.")
            .DisableAntiforgery();

        group
            .MapPost(
                "/link",
                async (IMediator mediator, WorkspaceId workspaceId, ProjectId? projectId, ProjectTaskId? taskId, UploadFileLinkRequest request) =>
                {
                    request = request with { WorkspaceId = workspaceId, ProjectId = projectId, TaskId = taskId };
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Upload file and generate download url.");

        group.MapGet(
            "/",
            async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceAssetsRequest request) =>
            {
                var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                return result.ToPlatformApiResult();
            }
        );

        group
            .MapDelete(
                "/asset/{assetId}",
                async (IMediator mediator, WorkspaceId workspaceId, WorkspaceAssetId assetId) =>
                {
                    var request = new DeleteWorkspaceAssetRequest(workspaceId, assetId);
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Delete a workspace asset");

        group
            .MapPatch(
                "/asset/{assetId}",
                async (IMediator mediator, WorkspaceId workspaceId, WorkspaceAssetId assetId, UpdateWorkspaceAssetRequest request) =>
                {
                    request = request with { WorkspaceId = workspaceId, AssetId = assetId };
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update a workspace asset");

        group
            .MapGet(
                "/asset/{assetId}/download",
                async (IMediator mediator, WorkspaceId workspaceId, WorkspaceAssetId assetId, [FromQuery] bool isPreview = true) =>
                {
                    var request = new DownloadAssetRequest
                    {
                        WorkspaceId = workspaceId,
                        AssetId = assetId,
                        IsPreview = isPreview,
                    };

                    var result = await mediator.Send(request);
                    if (!result.IsSuccess)
                        return Results.BadRequest(result.Errors);

                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Download a workspace asset");
    }
}
