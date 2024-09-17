using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberSkills.DeleteMemberSkill;

/// <summary>
/// Handles the request to delete a member skill.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The result of the operation.</returns>
public class DeleteMemberSkillHandler(IContext context) : IRequestHandler<DeleteMemberSkillRequest, Result>
{
    /// <summary>
    /// Handles the request to delete a member skill.
    /// </summary>
    /// <param name="request">The request to delete a member skill.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<Result> Handle(DeleteMemberSkillRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var skill = memberProfile.Skills?.FirstOrDefault(e => e.Identifier == request.MemberSkillId);

        if (skill == null)
        {
            return Result.NotFound("Member skill not found.");
        }

        memberProfile.Skills?.Remove(skill);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
