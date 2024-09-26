using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Accounts.UpdateUserFullName;

/// <summary>
/// Validator for the <see cref="UpdateUserFullNameRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UpdateUserFullNameValidator : AbstractValidator<UpdateUserFullNameRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserFullNameValidator"/> class.
    /// </summary>
    public UpdateUserFullNameValidator()
    {
        RuleFor(x => x.FullName).NotNull().NotEmpty().MaximumLength(StringSizes.Title);
    }
}
