using FluentValidation;
using Platform.Application.Features.Languages.UpdateLanguage;

namespace Platform.Application.Features.Languages.CreateLanguage;

/// <summary>
/// Validator for the UpdateLanguageRequest.
/// </summary>
public class UpdateLanguageValidator : AbstractValidator<UpdateLanguageRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateLanguageValidator"/> class.
    /// </summary>
    public UpdateLanguageValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Language name is required.")
            .MaximumLength(100)
            .WithMessage("Language name must not exceed 100 characters.");

        RuleFor(x => x.ProficiencyLevel).IsInEnum();
    }
}
