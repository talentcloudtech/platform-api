using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Tasks.CreateTask;

/// <summary>
/// Validator for the <see cref="CreateTaskRequest"/> class.
/// </summary>
public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskValidator"/> class.
    /// </summary>
    public CreateTaskValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.Estimate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(x => x.StartDate);
    }
}
