using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Educations.CreateEducation;
using Platform.Application.Features.Educations.DeleteEducation;
using Platform.Application.Features.Educations.UpdateEducation;
using Platform.Application.Features.Experiences.CreateExperience;
using Platform.Application.Features.Experiences.DeleteExperience;
using Platform.Application.Features.Experiences.UpdateExperience;
using Platform.Application.Features.Languages.CreateLanguage;
using Platform.Application.Features.Languages.DeleteLanguage;
using Platform.Application.Features.Languages.UpdateLanguage;
using Platform.Application.Features.MemberProfiles.GetMemberProfileById;
using Platform.Application.Features.MemberProfiles.UpdateAvatar;
using Platform.Application.Features.MemberProfiles.UpdateMemberProfile;
using Platform.Application.Features.MemberSkills.CreateMemberSkill;
using Platform.Application.Features.MemberSkills.DeleteMemberSkill;
using Platform.Application.Features.MemberSkills.UpdateMemberSkill;
using Platform.Application.Features.ProjectInfos.CreateProjectInfo;
using Platform.Application.Features.ProjectInfos.DeleteProjectInfo;
using Platform.Application.Features.ProjectInfos.UpdateProjectInfo;
using Platform.Domain.Auth.Interfaces;

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

        //group
        //    .MapGet(
        //        "/",
        //        async (IMediator mediator, [AsParameters] SearchMemberProfilesRequest request) =>
        //        {
        //            var result = await mediator.Send(request);
        //            return result;
        //        }
        //    )
        //    .WithSummary("Searches for member profiles.");

        group
            .MapGet(
                "/",
                async (IMediator mediator, ICurrentSession currentSession) =>
                {
                    var result = await mediator.Send(new GetMemberProfileByIdRequest(currentSession.UserId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Retrieves a member profile by its unique identifier.");

        group
            .MapPut(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, UpdateMemberProfileRequest request) =>
                {
                    var result = await mediator.Send(request with { Id = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a member profile by its unique identifier.");

        group
            .MapPatch(
                "/avatar",
                async (IMediator mediator, ICurrentSession currentSession, IFormFile file) =>
                {
                    var result = await mediator.Send(new UpdateAvatarRequest { Id = currentSession.UserId, File = file });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Update user avatar.")
            .DisableAntiforgery();

        // Experience endpoints
        group.MapGroup("experiences").WithTags("Experience").MapExperienceEndpoints();

        // Education endpoints
        group.MapGroup("educations").WithTags("Education").MapEducationEndpoints();

        // Language endpoints
        group.MapGroup("languages").WithTags("Language").MapLanguageEndpoints();

        // ProjectInfo endpoints
        group.MapGroup("projects").WithTags("ProjectInfo").MapProjectInfoEndpoints();

        // MemberSkill endpoints
        group.MapGroup("skills").WithTags("MemberSkill").MapMemberSkillEndpoints();
    }

    /// <summary>
    /// Maps the endpoints for experience.
    /// </summary>
    /// <param name="group">The route group builder.</param>
    /// <returns>The route group builder with experience endpoints.</returns>
    private static RouteGroupBuilder MapExperienceEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new experience for a member profile.");

        group
            .MapPut(
                "{experienceId}",
                async (IMediator mediator, ICurrentSession currentSession, string experienceId, UpdateExperienceRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, ExperienceId = experienceId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates an experience for a member profile.");

        group
            .MapDelete(
                "{experienceId}",
                async (IMediator mediator, ICurrentSession currentSession, string experienceId) =>
                {
                    var result = await mediator.Send(new DeleteExperienceRequest(currentSession.UserId, experienceId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes an experience from a member profile.");

        return group;
    }

    /// <summary>
    /// Maps the endpoints for education.
    /// </summary>
    /// <param name="group">The route group builder.</param>
    /// <returns>The route group builder with education endpoints.</returns>
    private static RouteGroupBuilder MapEducationEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateEducationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new education for a member profile.");

        group
            .MapPut(
                "{educationId}",
                async (IMediator mediator, ICurrentSession currentSession, string educationId, UpdateEducationRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, EducationId = educationId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates an education for a member profile.");

        group
            .MapDelete(
                "{educationId}",
                async (IMediator mediator, ICurrentSession currentSession, string educationId) =>
                {
                    var result = await mediator.Send(new DeleteEducationRequest(currentSession.UserId, educationId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes an education from a member profile.");

        return group;
    }

    /// <summary>
    /// Maps the endpoints for languages.
    /// </summary>
    /// <param name="group">The route group builder.</param>
    /// <returns>The route group builder with language endpoints.</returns>

    private static RouteGroupBuilder MapLanguageEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateLanguageRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new language for a member profile.");

        group
            .MapPut(
                "{languageId}",
                async (IMediator mediator, ICurrentSession currentSession, string languageId, UpdateLanguageRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, LanguageId = languageId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a language for a member profile.");

        group
            .MapDelete(
                "{languageId}",
                async (IMediator mediator, ICurrentSession currentSession, string languageId) =>
                {
                    var result = await mediator.Send(new DeleteLanguageRequest(currentSession.UserId, languageId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a language from a member profile.");

        return group;
    }

    /// <summary>
    /// Maps the endpoints for project information.
    /// </summary>
    /// <param name="group">The route group builder.</param>
    /// <returns></returns>
    private static RouteGroupBuilder MapProjectInfoEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateProjectInfoRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new project for a member profile.");

        group
            .MapPut(
                "{projectId}",
                async (IMediator mediator, ICurrentSession currentSession, string projectId, UpdateProjectInfoRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId, ProjectInfoId = projectId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a project for a member profile.");

        group
            .MapDelete(
                "{projectId}",
                async (IMediator mediator, ICurrentSession currentSession, string projectId) =>
                {
                    var result = await mediator.Send(new DeleteProjectInfoRequest(currentSession.UserId, projectId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a project from a member profile.");

        return group;
    }

    /// <summary>
    /// Maps the endpoints for member skills.
    /// </summary>
    /// <param name="group">The route group builder.</param>
    /// <returns>The route group builder with member skill endpoints.</returns>
    private static RouteGroupBuilder MapMemberSkillEndpoints(this RouteGroupBuilder group)
    {
        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession currentSession, CreateMemberSkillRequest request) =>
                {
                    var result = await mediator.Send(request with { MemberProfileId = currentSession.UserId });
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a new skill for a member profile.");

        group
            .MapPut(
                "{memberSkillId}",
                async (IMediator mediator, ICurrentSession currentSession, string memberSkillId, UpdateMemberSkillRequest request) =>
                {
                    var result = await mediator.Send(
                        request with
                        {
                            MemberProfileId = currentSession.UserId,
                            MemberSkillId = memberSkillId,
                            SkillId = request.SkillId,
                        }
                    );
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Updates a skill for a member profile.");

        group
            .MapDelete(
                "{memberSkillId}",
                async (IMediator mediator, ICurrentSession currentSession, string memberSkillId) =>
                {
                    var result = await mediator.Send(new DeleteMemberSkillRequest(currentSession.UserId, memberSkillId));
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Deletes a skill from a member profile.");

        return group;
    }
}
