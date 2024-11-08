using FluentValidation;

namespace Platform.API.Endpoints.TaskAssignees.UpdateTaskAssignee;

/// <summary>
/// Validator for the UpdateTaskAssigneeRequest
/// </summary>
public class UpdateTaskAssigneeValidator : AbstractValidator<UpdateTaskAssigneeRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateTaskAssigneeValidator class
    /// </summary>
    public UpdateTaskAssigneeValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty().WithMessage("Workspace ID is required");

        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project ID is required");

        RuleFor(x => x.TaskId).NotEmpty().WithMessage("Task ID is required");

        RuleFor(x => x.UserIds).NotNull().WithMessage("User IDs list is required");
    }
}
