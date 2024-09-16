using FluentValidation;

namespace Platform.Application.Features.Languages.CreateLanguage;

/// <summary>
/// Validator for the CreateLanguageRequest.
/// </summary>
public class CreateLanguageValidator : AbstractValidator<CreateLanguageRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateLanguageValidator"/> class.
    /// </summary>
    public CreateLanguageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Language name is required.")
            .MaximumLength(100)
            .WithMessage("Language name must not exceed 100 characters.");

        RuleFor(x => x.ProficiencyLevel).IsInEnum().WithMessage("Invalid proficiency level.");
    }
}
