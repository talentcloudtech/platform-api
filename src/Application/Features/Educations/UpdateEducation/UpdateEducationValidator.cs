using FluentValidation;

namespace Platform.Application.Features.Educations.UpdateEducation;

/// <summary>
/// Validates the request to update an education.
/// </summary>
public class UpdateEducationValidator : AbstractValidator<UpdateEducationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateEducationValidator"/> class.
    /// </summary>
    public UpdateEducationValidator()
    {
        RuleFor(x => x.EducationId).NotEmpty().WithMessage("Education id is required.");
        RuleFor(x => x.InstitutionName).NotEmpty().WithMessage("Institution name is required.");
        RuleFor(x => x.Degree).NotEmpty().WithMessage("Degree is required.");
        RuleFor(x => x.FieldOfStudy);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        RuleFor(x => x.EndDate)
            .Must((request, endDate) => !endDate.HasValue || endDate.Value >= request.StartDate)
            .WithMessage("End date must be greater than or equal to the start date.");
    }
}
