using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Projects.CreateProject;

/// <summary>
/// Validator for the <see cref="CreateProjectRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectValidator"/> class.
    /// </summary>
    public CreateProjectValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
    }
}
