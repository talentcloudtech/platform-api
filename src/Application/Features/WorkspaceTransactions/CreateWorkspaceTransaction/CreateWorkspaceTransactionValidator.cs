using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;

/// <summary>
/// Validator for the <see cref="CreateWorkspaceTransactionRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateWorkspaceValidator : AbstractValidator<CreateWorkspaceTransactionRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWorkspaceValidator"/> class.
    /// </summary>
    public CreateWorkspaceValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
    }
}
