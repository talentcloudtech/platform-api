using FluentValidation;

namespace Platform.Application.Features.WorkspaceMembers.UpdateWorkspaceMember;

/// <summary>
/// Validator for the <see cref="UpdateWorkspaceMemberRequest"/> class, ensuring that the necessary fields are provided and correctly formatted.
/// </summary>
public class UpdateWorkspaceMemberValidator : AbstractValidator<UpdateWorkspaceMemberRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWorkspaceMemberValidator"/> class.
    /// </summary>
    public UpdateWorkspaceMemberValidator()
    {
        RuleFor(x => x.WorkspaceId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
    }
}
