using FluentValidation;
using Platform.Application.Common;

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
        RuleFor(x => x.Name).NotEmpty().WithMessage("Language name is required.").MaximumLength(StringSizes.Title);

        RuleFor(x => x.ProficiencyLevel).IsInEnum().WithMessage("Invalid proficiency level.");
    }
}
