using FluentValidation;

namespace Platform.Application.Features.MemberSkills.CreateMemberSkill;

/// <summary>
/// Validates the request to create a new member skill.
/// </summary>
public class CreateMemberSkillValidator : AbstractValidator<CreateMemberSkillRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberSkillValidator"/> class.
    /// </summary>
    public CreateMemberSkillValidator()
    {
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("SkillId is required.");
        RuleFor(x => x.MonthsOfExperience).NotEmpty().WithMessage("MonthsOfExperience is required.");
    }
}
