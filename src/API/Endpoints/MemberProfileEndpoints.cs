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
using Platform.Application.Features.Languages.CreateLanguage;
using Platform.Application.Features.Languages.DeleteLanguage;
using Platform.Application.Features.Languages.GetLanguageById;
using Platform.Application.Features.Languages.UpdateLanguage;
using Platform.Application.Features.MemberProfiles.DeleteMemberProfile;
using Platform.Application.Features.MemberProfiles.GetMemberProfileById;
using Platform.Application.Features.MemberProfiles.SearchMemberProfiles;
using Platform.Application.Features.MemberProfiles.UpdateAvatar;
using Platform.Application.Features.MemberProfiles.UpdateMemberProfile;
using Platform.Application.Features.ProjectInfos.CreateProjectInfo;
using Platform.Application.Features.ProjectInfos.DeleteProjectInfo;
using Platform.Application.Features.ProjectInfos.GetProjectInfoById;
using Platform.Application.Features.ProjectInfos.UpdateProjectInfo;
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
            .MapPatch(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, IFormFile file) =>
                {
                    var result = await mediator.Send(new UpdateAvatarRequest { Id = currentSession.UserId, File = file });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Update user avatar.")
            .DisableAntiforgery();

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

        // Language endpoints
        group.MapGroup("languages").WithTags("Language").MapLanguageEndpoints();

        // ProjectInfo endpoints
        group.MapGroup("projects").WithTags("ProjectInfo").MapProjectInfoEndpoints();
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

    private static RouteGroupBuilder MapLanguageEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "{languageId}",
                async (IMediator mediator, ICurrentSession currentSession, string languageId) =>
                {
                    var result = await mediator.Send(new GetLanguageByIdRequest(currentSession.UserId, languageId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a specific language for a member profile.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateLanguageRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new language for a member profile.");

        group
            .MapPut(
                "{languageId}",
                async (IMediator mediator, ICurrentSession currentSession, string languageId, UpdateLanguageRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, LanguageId = languageId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates a language for a member profile.");

        group
            .MapDelete(
                "{languageId}",
                async (IMediator mediator, ICurrentSession currentSession, string languageId) =>
                {
                    var result = await mediator.Send(new DeleteLanguageRequest(currentSession.UserId, languageId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes a language from a member profile.");

        return group;
    }

    private static RouteGroupBuilder MapProjectInfoEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapGet(
                "{projectId}",
                async (IMediator mediator, ICurrentSession currentSession, string projectId) =>
                {
                    var result = await mediator.Send(new GetProjectInfoByIdRequest(currentSession.UserId, projectId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Retrieves a specific project for a member profile.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateProjectInfoRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Creates a new project for a member profile.");

        group
            .MapPut(
                "{projectId}",
                async (IMediator mediator, ICurrentSession currentSession, string projectId, UpdateProjectInfoRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, ProjectInfoId = projectId });
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Updates a project for a member profile.");

        group
            .MapDelete(
                "{projectId}",
                async (IMediator mediator, ICurrentSession currentSession, string projectId) =>
                {
                    var result = await mediator.Send(new DeleteProjectInfoRequest(currentSession.UserId, projectId));
                    return result.ToMinimalApiResult();
                }
            )
            .WithSummary("Deletes a project from a member profile.");

        return group;
    }
}
