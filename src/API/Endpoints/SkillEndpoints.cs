using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Platform.Application.Extensions;
using Platform.Application.Features.Skills.CreateSkill;
using Platform.Application.Features.Skills.SearchSkills;
using Platform.Domain.Auth.Interfaces;

namespace Platform.API.Endpoints;

/// <summary>
/// Represents the endpoints for skills.
/// </summary>
public class SkillEndpoints : IEndpoints
{
    /// <summary>
    /// Maps the endpoints for skills.
    /// </summary>
    /// <param name="builder">The endpoint route builder.</param>
    public static void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/skills").WithTags("Skills").RequireAuthorization();

        group
            .MapGet(
                "/",
                async (IMediator mediator, [AsParameters] SearchSkillsRequest request) =>
                {
                    var result = await mediator.Send(request);
                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Searches for skills.");

        group
            .MapPost(
                "/",
                async (IMediator mediator, ICurrentSession session, CreateSkillRequest request) =>
                {
                    var result = await mediator.Send(request);

                    return result.ToPlatformApiResult();
                }
            )
            .WithSummary("Creates a skill.");
    }
}
