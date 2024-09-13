using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.MemberProfiles.CreateMemberProfile;

/// <summary>
/// Validator for the <see cref="CreateMemberProfileRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateMemberProfileValidator : AbstractValidator<CreateMemberProfileRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberProfileValidator"/> class.
    /// </summary>
    public CreateMemberProfileValidator()
    {
        RuleLevelCascadeMode = ClassLevelCascadeMode;

        RuleFor(x => x.FullName).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.ShortBio).NotEmpty().MaximumLength(StringSizes.Bio);
        RuleFor(x => x.Timezone).NotEmpty();
        RuleFor(x => x.ProfilePictureUrl).NotEmpty();
        RuleFor(x => x.ResumeFileUrl).NotEmpty();
    }
}
