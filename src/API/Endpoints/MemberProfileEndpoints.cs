using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Features.Experiences.CreateExperience;
using Platform.Application.Features.Experiences.DeleteExperience;
using Platform.Application.Features.Experiences.GetExperienceById;
using Platform.Application.Features.Experiences.UpdateExperience;
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
                async (IMediator mediator, HttpContext context, [AsParameters] SearchMemberProfilesRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result;
                }
            )
            .WithSummary("Searches for member profiles.");

        group
            .MapGet(
                "{id}",
                async (IMediator mediator, UserId id) =>
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
                async (IMediator mediator, UserId id, UpdateMemberProfileRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = id });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates a member profile by its unique identifier.");

        group
            .MapDelete(
                "{id}",
                async (IMediator mediator, UserId id) =>
                {
                    var result = await mediator.Send(new DeleteMemberProfileRequest(id));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes a member profile by its unique identifier.");

        // Experience endpoints
        group.MapGroup("{id}/experiences").WithTags("Experience").RequireAuthorization().MapExperienceEndpoints();
    }

    private static RouteGroupBuilder MapExperienceEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "{experienceId}",
                async (IMediator mediator, UserId id, string experienceId) =>
                {
                    var result = await mediator.Send(new GetExperienceByIdRequest(id, experienceId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a specific experience for a member profile.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, UserId id, CreateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = id });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new experience for a member profile.");

        group
            .MapPut(
                "{experienceId}",
                async (IMediator mediator, UserId id, string experienceId, UpdateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = id, ExperienceId = experienceId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates an experience for a member profile.");

        group
            .MapDelete(
                "{experienceId}",
                async (IMediator mediator, UserId id, string experienceId) =>
                {
                    var result = await mediator.Send(new DeleteExperienceRequest(id, experienceId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes an experience from a member profile.");

        return group;
    }
}
