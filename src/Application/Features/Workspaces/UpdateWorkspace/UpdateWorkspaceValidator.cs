using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Workspaces.UpdateWorkspace;

/// <summary>
/// Validator for the <see cref="UpdateWorkspaceRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UpdateWorkspaceValidator : AbstractValidator<UpdateWorkspaceRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWorkspaceValidator"/> class.
    /// </summary>
    public UpdateWorkspaceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.ContactEmail).EmailAddress().MaximumLength(StringSizes.Email);
        RuleFor(x => x.Address).MaximumLength(StringSizes.ShortAddress);
        RuleFor(x => x.Logo).MaximumLength(StringSizes.Url);
    }
}
