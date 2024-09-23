using FluentValidation;
using Platform.Application.Common;

namespace Platform.Application.Features.Tasks.UpdateTask;

/// <summary>
/// Validator for the <see cref="UpdateTaskRequest"/> class.
/// </summary>
public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskValidator"/> class.
    /// </summary>
    public UpdateTaskValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(StringSizes.Title);
        RuleFor(x => x.Description).MaximumLength(StringSizes.Max);
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Priority).IsInEnum();
        RuleFor(x => x.Estimate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.DueDate).GreaterThanOrEqualTo(x => x.StartDate);
    }
}
