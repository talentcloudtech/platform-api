using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Projects.UpdateProject;

/// <summary>
/// Represents a validator for the DeleteProjectRequest class.
/// </summary>
public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectValidator"/> class.
    /// </summary>
    public UpdateProjectValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().MaximumLength(StringSizes.Title);

        RuleFor(x => x.Status).IsInEnum();

        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
    }
}
