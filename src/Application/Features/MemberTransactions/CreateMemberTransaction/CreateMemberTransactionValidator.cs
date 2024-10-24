using FluentValidation;
using Platform.Application.Common;
using Platform.Application.Features.MemberTransactions.CreateMemberTransaction;

namespace Platform.Application.Features.WorkspaceTransactions.CreateWorkspaceTransaction;

/// <summary>
/// Validator for the <see cref="CreateWorkspaceTransactionRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateMemberTransactionValidator : AbstractValidator<CreateMemberTransactionRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberTransactionValidator"/> class.
    /// </summary>
    public CreateMemberTransactionValidator()
    {
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.PaymentMethod).NotEmpty();
    }
}
