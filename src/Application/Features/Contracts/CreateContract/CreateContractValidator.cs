using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Contracts.CreateContract;

/// <summary>
/// Validator for the <see cref="CreateContractRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateContractValidator : AbstractValidator<CreateContractRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContractValidator"/> class.
    /// </summary>
    public CreateContractValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.BillingType).IsInEnum();
        RuleFor(x => x.AvailablePerDay).GreaterThanOrEqualTo(0);
        RuleFor(x => x.BillingRate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThanOrEqualTo(x => x.StartDate);
        RuleFor(x => x.Status).IsInEnum();
    }
}
