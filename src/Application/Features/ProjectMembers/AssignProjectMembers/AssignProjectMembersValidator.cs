using FluentValidation;

namespace Platform.Application.Features.ProjectMembers.AssignProjectMembers;

/// <summary>
/// Validator for AssignProjectMembersRequest
/// </summary>
public class AssignProjectMembersValidator : AbstractValidator<AssignProjectMembersRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignProjectMembersValidator"/> class
    /// </summary>
    public AssignProjectMembersValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Project ID is required");

        RuleFor(x => x.WorkspaceId).NotEmpty().WithMessage("Workspace ID is required");

        RuleFor(x => x.UserIds).NotEmpty().WithMessage("At least one user must be specified");

        RuleFor(x => x.Role).IsInEnum().WithMessage("Invalid project role specified");
    }
}
