using FluentValidation;

namespace Platform.Application.Features.ProjectInfos.CreateProjectInfo;

/// <summary>
/// Validator for the CreateProjectInfoRequest.
/// </summary>
public class CreateProjectValidator : AbstractValidator<CreateProjectInfoRequest>
{
    /// <summary>
    /// Constructor for the CreateProjectValidator.
    /// </summary>
    public CreateProjectValidator()
    {
        RuleFor(p => p.Title).NotEmpty().MaximumLength(100);
        RuleFor(p => p.Link).MaximumLength(200);
        RuleFor(p => p.ShortDescription).MaximumLength(200);
    }
}
