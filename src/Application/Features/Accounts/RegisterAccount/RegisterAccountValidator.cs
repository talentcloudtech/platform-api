using FluentValidation;

namespace Platform.Application.Features.Accounts.RegisterAccount;

/// <summary>
/// Validator for the RegisterAccountRequest.
/// </summary>
public class RegisterAccountValidator : AbstractValidator<RegisterAccountRequest>
{
    /// <summary>
    /// Initializes a new instance of the RegisterAccountValidator class.
    /// </summary>
    public RegisterAccountValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}