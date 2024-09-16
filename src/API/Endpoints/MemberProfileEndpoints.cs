using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Features.Educations.CreateEducation;
using Platform.Application.Features.Educations.DeleteEducation;
using Platform.Application.Features.Educations.GetEducationById;
using Platform.Application.Features.Educations.UpdateEducation;
using Platform.Application.Features.Experiences.CreateExperience;
using Platform.Application.Features.Experiences.DeleteExperience;
using Platform.Application.Features.Experiences.GetExperienceById;
using Platform.Application.Features.Experiences.UpdateExperience;
using Platform.Application.Features.MemberProfiles.CreateMemberProfile;
using Platform.Application.Features.MemberProfiles.DeleteMemberProfile;
using Platform.Application.Features.MemberProfiles.GetMemberProfileById;
using Platform.Application.Features.MemberProfiles.SearchMemberProfiles;
using Platform.Application.Features.MemberProfiles.UpdateMemberProfile;
using Platform.Domain.Auth.Interfaces;
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
        group.MapGroup("experiences").WithTags("Experience").MapExperienceEndpoints();

        // Education endpoints
        group.MapGroup("educations").WithTags("Education").MapEducationEndpoints();
    }

    private static RouteGroupBuilder MapExperienceEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "{experienceId}",
                async (IMediator mediator, ICurrentSession currentSession, string experienceId) =>
                {
                    var result = await mediator.Send(new GetExperienceByIdRequest(currentSession.UserId, experienceId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a specific experience for a member profile.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new experience for a member profile.");

        group
            .MapPut(
                "{experienceId}",
                async (IMediator mediator, ICurrentSession currentSession, string experienceId, UpdateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, ExperienceId = experienceId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates an experience for a member profile.");

        group
            .MapDelete(
                "{experienceId}",
                async (IMediator mediator, ICurrentSession currentSession, string experienceId) =>
                {
                    var result = await mediator.Send(new DeleteExperienceRequest(currentSession.UserId, experienceId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes an experience from a member profile.");

        return group;
    }

    private static RouteGroupBuilder MapEducationEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "{educationId}",
                async (IMediator mediator, ICurrentSession currentSession, string educationId) =>
                {
                    var result = await mediator.Send(new GetEducationByIdRequest(currentSession.UserId, educationId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a specific education for a member profile.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateEducationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new education for a member profile.");

        group
            .MapPut(
                "{educationId}",
                async (IMediator mediator, ICurrentSession currentSession, string educationId, UpdateEducationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, EducationId = educationId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates an education for a member profile.");

        group
            .MapDelete(
                "{educationId}",
                async (IMediator mediator, ICurrentSession currentSession, string educationId) =>
                {
                    var result = await mediator.Send(new DeleteEducationRequest(currentSession.UserId, educationId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes an education from a member profile.");

        return group;
    }
}
