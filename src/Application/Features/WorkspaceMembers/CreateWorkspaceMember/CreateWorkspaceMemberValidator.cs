using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.WorkspaceMembers.CreateWorkspaceMember;

/// <summary>
/// Validator for the <see cref="CreateWorkspaceMemberRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateWorkspaceMemberValidator : AbstractValidator<CreateWorkspaceMemberRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWorkspaceMemberValidator"/> class.
    /// </summary>
    public CreateWorkspaceMemberValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.Emails).NotEmpty().ForEach(email => email.EmailAddress().MaximumLength(StringSizes.Email));
        RuleFor(x => x.Role).IsInEnum();
    }
}
