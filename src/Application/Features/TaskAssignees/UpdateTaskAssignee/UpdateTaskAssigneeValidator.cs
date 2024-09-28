using FluentValidation;

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Validator for the <see cref="UpdateTaskAssigneeRequest"/> class.
/// </summary>
public class UpdateTaskAssigneeValidator : AbstractValidator<UpdateTaskAssigneeRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskAssigneeValidator"/> class.
    /// </summary>
    public UpdateTaskAssigneeValidator()
    {
        RuleFor(x => x.WorkspaceId).NotNull().NotEmpty();
        RuleFor(x => x.ProjectId).NotNull().NotEmpty();
        RuleFor(x => x.TaskId).NotNull().NotEmpty();
        RuleFor(x => x.AssigneeId).NotNull().NotEmpty();
        RuleFor(x => x.TimeSpent).NotNull().NotEmpty().GreaterThan(0);
    }
}
