using FluentValidation;

namespace Platform.Application.Features.MemberSkills.UpdateMemberSkill;

/// <summary>
/// Validates the request to update a member skill.
/// </summary>
public class UpdateMemberSkillValidator : AbstractValidator<UpdateMemberSkillRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMemberSkillValidator"/> class.
    /// </summary>
    public UpdateMemberSkillValidator()
    {
        RuleFor(x => x.SkillId).NotNull().NotEmpty().WithMessage("Skill ID is required.");
        RuleFor(x => x.MonthsOfExperience).NotEmpty().GreaterThan(0).WithMessage("Months of experience is required.");
    }
}
