using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberSkills.GetMemberSkillById;

/// <summary>
/// Handles the request to get a member skill by its ID.
/// </summary>
/// <param name="context">The database context.</param>
/// <returns>The member skill response.</returns>
public class GetMemberSkillByIdHandler(IContext context) : IRequestHandler<GetMemberSkillByIdRequest, Result<MemberSkillResponse>>
{
    /// <summary>
    /// Handles the request to get a member skill by its identifier.
    /// </summary>
    /// <param name="request">The request to get a member skill.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The member skill response.</returns>
    public async Task<Result<MemberSkillResponse>> Handle(GetMemberSkillByIdRequest request, CancellationToken cancellationToken)
    {
        var memberProfile = await context.MemberProfiles.FirstOrDefaultAsync(mp => mp.Id == request.MemberProfileId, cancellationToken);

        if (memberProfile == null)
        {
            return Result.NotFound("Member profile not found.");
        }

        var memberSkill = memberProfile.Skills?.FirstOrDefault(x => x.SkillId == request.MemberSkillId);
        if (memberSkill == null)
        {
            return Result.NotFound("Member skill not found.");
        }

        return Result.Success(memberSkill.Adapt<MemberSkillResponse>());
    }
}
