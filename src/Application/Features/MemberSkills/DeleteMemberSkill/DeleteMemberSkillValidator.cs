using FluentValidation;
using Platform.Application.Features.Languages.DeleteLanguage;

namespace Platform.Application.Features.MemberSkills.DeleteMemberSkill;

/// <summary>
/// Validates the request to delete a member skill.
/// </summary>
public class DeleteMemberSkillValidator : AbstractValidator<DeleteMemberSkillRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMemberSkillValidator"/> class.
    /// </summary>
    public DeleteMemberSkillValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.MemberSkillId).NotEmpty();
    }
}
