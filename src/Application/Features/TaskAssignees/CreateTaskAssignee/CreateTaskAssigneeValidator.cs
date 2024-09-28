using FluentValidation;

namespace Platform.API.Endpoints.TaskAssignees.CreateTaskAssignee;

/// <summary>
/// Validator for the <see cref="CreateTaskAssigneeRequest"/> class.
/// </summary>
public class CreateTaskAssigneeValidator : AbstractValidator<CreateTaskAssigneeRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskAssigneeValidator"/> class.
    /// </summary>
    public CreateTaskAssigneeValidator()
    {
        RuleFor(x => x.WorkspaceId).NotNull().NotEmpty();
        RuleFor(x => x.ProjectId).NotNull().NotEmpty();
        RuleFor(x => x.TaskId).NotNull().NotEmpty();
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}
