using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Skills.CreateSkill;

/// <summary>
/// Validates the request to create a new skill.
/// </summary>
public class CreateSkillValidator : AbstractValidator<CreateSkillRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSkillValidator"/> class.
    /// </summary>
    public CreateSkillValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(StringSizes.Short);
    }
}
