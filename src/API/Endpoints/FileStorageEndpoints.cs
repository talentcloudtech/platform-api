using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.FileStorages.GenerateSasUri;
using Platform.Application.Features.FileStorages.UploadFile;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for file storage.
/// </summary>
public class FileStorageEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for file storage.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/fileStorage").WithTags("FileStorage").RequireAuthorization();

        group
            .MapPost(
                "/",
                async (IMediator mediator, IFormFile file) =>
                {
                    var request = new UploadFileRequest { File = file };
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Upload file and generate download url.")
            .DisableAntiforgery();

        group
            .MapGet(
                "generateDownloadUrl/{filePath}",
                async (IMediator mediator, string filePath) =>
                {
                    var result = await mediator.Send(new GenerateSasUriRequest(filePath));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Generate download url.");
    }
}
