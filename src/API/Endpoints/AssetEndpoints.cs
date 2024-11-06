using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.FileStorages.GenerateSasUri;
using Platform.Application.Features.FileStorages.UploadWorkspaceFile;
using Platform.Application.Features.WorkspaceAssets.SearchWorkspaceAssets;
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

        group.MapGet(
            "/",
            async (IMediator mediator, WorkspaceId workspaceId, [AsParameters] SearchWorkspaceAssetsRequest request) =>
            {
                var result = await mediator.Send(request with { WorkspaceId = workspaceId });
                return result.ToPlatformApiResult();
            }
        );
    }
}
