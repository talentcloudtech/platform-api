using Ardalis.Result.AspNetCore;
using MediatR;
using Platform.Application.Features.MemberProfiles.CreateMemberProfile;
using Platform.Application.Features.MemberProfiles.DeleteMemberProfile;
using Platform.Application.Features.MemberProfiles.GetMemberProfileById;
using Platform.Application.Features.MemberProfiles.SearchMemberProfiles;
using Platform.Application.Features.MemberProfiles.UpdateMemberProfile;
using Platform.Domain.Entities.Common;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for member profiles.
/// </summary>
public static class MemberProfileEndpoints
{
    /// <summary>
    /// Maps the endpoints for member profiles.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapMemberProfileEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/memberProfiles").WithTags("MemberProfile").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchMemberProfilesRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result;
                }
            )
            .WithSummary("Searches for member profiles.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, Guid id) =>
                {
                    var result = await mediator.Send(new GetMemberProfileByIdRequest(id));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a member profile by its unique identifier.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, CreateMemberProfileRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new member profile.");

        group
            .MapPut(
                "{id}",
                async (IMediator mediator, Guid id, UpdateMemberProfileRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = id });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates a member profile by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, Guid id) =>
                {
                    var result = await mediator.Send(new DeleteMemberProfileRequest(id));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes a member profile by its unique identifier.");
    }
}
