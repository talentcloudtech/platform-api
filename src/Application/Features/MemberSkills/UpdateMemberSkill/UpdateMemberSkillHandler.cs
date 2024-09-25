using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberSkills.UpdateMemberSkill;

/// <summary>
/// Handles the request to update an existing member skill.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class UpdateMemberSkillHandler(IContext context) : IRequestHandler<UpdateMemberSkillRequest, Result<MemberSkillResponse>>
{
    /// <summary>
    /// Handles the request to update a member skill.
    /// </summary>
    /// <param name="request">The request containing the member skill details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result<MemberSkillResponse>> Handle(UpdateMemberSkillRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);
        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }
        var memberSkill = memberProfile.Skills?.FirstOrDefault(x => x.Identifier == request.MemberSkillId);
        if (memberSkill == null)
        {
            return Result.NotFound("Member skill not found.");
        }
        var skill = await context.Skills.FirstOrDefaultAsync(sk => sk.Id == request.SkillId, cancellationToken);
        if (skill == null)
        {
            return Result.NotFound("Skill not found.");
        }
        var existMemberSkill = memberProfile.Skills?.FirstOrDefault(x => x.Identifier != request.MemberSkillId && x.SkillId == request.SkillId);
        if (existMemberSkill != null)
        {
            return Result.Conflict("Duplicate skill found");
        }

        memberSkill.SkillId = request.SkillId;
        memberSkill.Name = skill.Name;
        memberSkill.MonthsOfExperience = request.MonthsOfExperience;

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(memberSkill.Adapt<MemberSkillResponse>());
    }
}
