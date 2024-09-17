using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Platform.Application.Common;
using Platform.Domain.Entities;

namespace Platform.Application.Features.Skills.CreateSkill;

/// <summary>
/// Handles the request to create a new skill entry.
/// <pram name="context">The database context.</pram>
/// </summary>
public class CreateSkillHandler(IContext context) : IRequestHandler<CreateSkillRequest, Result<SkillResponse>>
{
    /// <summary>
    /// Handles the request to create a new skill.
    /// </summary>
    /// <param name="request">The request to create a skill.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created skill response.</returns>
    public async Task<Result<SkillResponse>> Handle(CreateSkillRequest request, CancellationToken cancellationToken)
    {
        var existSkillName = await context.Skills.FirstOrDefaultAsync(
            predicate: p => p.Name.ToLower().Trim() == request.Name.ToLower().Trim(),
            cancellationToken
        );
        if (existSkillName != null)
        {
            return Result.Conflict("Duplicate skill name found.");
        }
        var skill = new Skill { Name = request.Name };

        await context.Skills.AddAsync(skill, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(skill.Adapt<SkillResponse>());
    }
}
