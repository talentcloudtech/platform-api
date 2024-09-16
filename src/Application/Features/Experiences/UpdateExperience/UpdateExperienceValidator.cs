using FluentValidation;

namespace Platform.Application.Features.Experiences.UpdateExperience;

/// <summary>
/// Validator for the <see cref="UpdateExperienceRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UpdateExperienceValidator : AbstractValidator<UpdateExperienceRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateExperienceValidator"/> class.
    /// </summary>
    public UpdateExperienceValidator()
    {
        RuleFor(x => x.ExperienceId).NotEmpty().WithMessage("Experience id is required.");
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Company name is required.");
        RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Industry).NotEmpty().WithMessage("Industry is required.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required.");
        RuleFor(x => x.EndDate)
            .Must((request, endDate) => !endDate.HasValue || endDate.Value >= request.StartDate)
            .WithMessage("End date must be greater than or equal to the start date.");
    }
}
