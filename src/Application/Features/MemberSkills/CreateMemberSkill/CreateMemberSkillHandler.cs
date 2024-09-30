using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Application.Features.MemberProfiles;
using Platform.Domain.Entities;
using Platform.Domain.Entities.Common;

namespace Platform.Application.Features.MemberSkills.CreateMemberSkill;

/// <summary>
/// Handles the request to create a new member skill.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The created member skill response.</returns>
public class CreateMemberSkillHandler(IContext context) : IRequestHandler<CreateMemberSkillRequest, Result<MemberSkillResponse>>
{
    /// <summary>
    /// Handles the request to create a new member skill.
    /// </summary>
    /// <param name="request">The request to create a member skill.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created member skill response.</returns>
    public async Task<Result<MemberSkillResponse>> Handle(CreateMemberSkillRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            // return Result.NotFound("Member profile not found.");
            memberProfile = new MemberProfile();
            memberProfile.Id = request.MemberProfileId;
            context.MemberProfiles.Add(memberProfile);
            await context.SaveChangesAsync(cancellationToken);
        }

        var skill = await context.Skills.FirstOrDefaultAsync(sk => sk.Id == request.SkillId, cancellationToken);
        if (skill == null)
        {
            return Result.NotFound("Skill not found.");
        }

        var existMemberSkill = memberProfile.Skills?.FirstOrDefault(x => x.SkillId == request.SkillId);
        if (existMemberSkill != null)
        {
            return Result.Conflict("Duplicate skill found");
        }

        var memberSkill = new MemberSkill
        {
            Identifier = CustomNewId.GenerateStandardId(),
            SkillId = request.SkillId,
            Name = skill.Name,
            MonthsOfExperience = request.MonthsOfExperience,
        };
        if (memberProfile.Skills == null)
            memberProfile.Skills = new List<MemberSkill>();

        memberProfile.Skills?.Add(memberSkill);
        memberProfile.IsProfileComplete = ProfileScoreCalculator.IsProfileComplete(memberProfile);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(memberSkill.Adapt<MemberSkillResponse>());
    }
}
