using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Workspaces.CreateWorkspace;

/// <summary>
/// Validator for the <see cref="CreateWorkspaceRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateWorkspaceValidator : AbstractValidator<CreateWorkspaceRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWorkspaceValidator"/> class.
    /// </summary>
    public CreateWorkspaceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.ContactEmail).NotEmpty().MaximumLength(StringSizes.Email);
        RuleFor(x => x.Address).MaximumLength(StringSizes.ShortAddress);
        RuleFor(x => x.Logo).MaximumLength(StringSizes.Url);
    }
}
