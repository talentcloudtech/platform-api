using FluentValidation;

namespace Platform.Application.Features.Experiences.DeleteExperience;

/// <summary>
/// Represents a validator for the DeleteExperienceRequest class.
/// </summary>
public class DeleteExperienceValidator : AbstractValidator<DeleteExperienceRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteExperienceValidator"/> class.
    /// </summary>
    public DeleteExperienceValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.ExperienceId).NotEmpty();
    }
}
