using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Accounts.UserRegister;

/// <summary>
/// Validator for the <see cref="UserRegisterRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisterValidator"/> class.
    /// </summary>
    public UserRegisterValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.UserName).NotEmpty().MaximumLength(25);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(25);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}
