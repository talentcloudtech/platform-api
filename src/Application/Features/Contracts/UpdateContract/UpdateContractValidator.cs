using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Contracts.UpdateContract;

/// <summary>
/// Validator for the <see cref="UpdateContractRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UpdateContractValidator : AbstractValidator<UpdateContractRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateContractValidator"/> class.
    /// </summary>
    public UpdateContractValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.WorkspaceId).NotEmpty();
    }
}
