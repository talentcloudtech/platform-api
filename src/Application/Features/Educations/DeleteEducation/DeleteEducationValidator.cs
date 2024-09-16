using FluentValidation;

namespace Platform.Application.Features.Educations.DeleteEducation;

/// <summary>
/// Represents a validator for the DeleteEducationRequest class.
/// </summary>
public class DeleteEducationValidator : AbstractValidator<DeleteEducationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEducationValidator"/> class.
    /// </summary>
    public DeleteEducationValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.EducationId).NotEmpty();
    }
}
