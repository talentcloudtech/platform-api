using FluentValidation;

namespace Platform.Application.Features.Languages.DeleteLanguage;

/// <summary>
/// Represents a validator for the DeleteLanguageRequest class.
/// </summary>
public class DeleteLanguageValidator : AbstractValidator<DeleteLanguageRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteLanguageValidator"/> class.
    /// </summary>
    public DeleteLanguageValidator()
    {
        RuleFor(x => x.MemberProfileId).NotEmpty();
        RuleFor(x => x.LanguageId).NotEmpty();
    }
}
