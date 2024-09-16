using FluentValidation;
using Platform.Application.Features.MemberProfiles.DeleteMemberProfile;

namespace Platform.Application.Features.Experiences.DeleteExperience;

/// <summary>
/// Represents a validator for the DeleteExperienceRequest class.
/// </summary>
public class DeleteExperienceValidator : AbstractValidator<DeleteExperienceRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMemberProfileValidator"/> class.
    /// </summary>
    public DeleteExperienceValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.ExperienceId).NotEmpty();
    }
}
