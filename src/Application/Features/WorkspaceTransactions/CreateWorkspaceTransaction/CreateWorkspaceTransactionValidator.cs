using FluentValidation;

namespace Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;

/// <summary>
/// Validator for the <see cref="CreateWorkspaceTransactionRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateWorkspaceTransactionValidator : AbstractValidator<CreateWorkspaceTransactionRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWorkspaceTransactionValidator"/> class.
    /// </summary>
    public CreateWorkspaceTransactionValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
    }
}
