using FluentValidation;

namespace Platform.Application.Features.Educations.CreateEducation;

/// <summary>
/// Validates the request to create an education.
/// </summary>
public class CreateEducationValidator : AbstractValidator<CreateEducationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEducationValidator"/> class.
    /// </summary>
    public CreateEducationValidator()
    {
        RuleFor(x => x.InstitutionName).NotEmpty().WithMessage("Institution name is required.");
        RuleFor(x => x.Degree).NotEmpty().WithMessage("Degree is required.");
        RuleFor(x => x.FieldOfStudy);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        RuleFor(x => x.EndDate)
            .Must((request, endDate) => !endDate.HasValue || endDate.Value >= request.StartDate)
            .WithMessage("End date must be greater than or equal to the start date.");
    }
}
